using System.Security.Claims;
using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Helpers;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.Domain.Helpers;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Decenea.Application.Features.User.Commands.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, ErrorOr<LoginUserResponse>>
{
    private readonly IDeceneaDbContext _dbContext;
    private readonly IConfiguration _configuration;
    public LoginUserCommandHandler(IDeceneaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }
    public async Task<ErrorOr<LoginUserResponse>> ExecuteAsync(LoginUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var loginUserResponse = new LoginUserResponse();
            
            var user = await _dbContext.Set<Domain.Aggregates.UserAggregate.User>()
                .FirstOrDefaultAsync(i=> i.Email == command.Email, cancellationToken);
            
            if(user is null)
                return Error.NotFound(description: "User not found.");

            if(user.LockoutEnabled)
                return Error.Failure(description: "User is locked.");
            
            _dbContext.ModifiedBy = user.Id;

            var passCheck = CheckPassword(command.Password, user.PasswordHash);
            
            if (passCheck.IsError)
            {
                return passCheck.Errors;
            }

            var accessTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            var jwtToken = JWTBearer.CreateToken(
                signingKey: _configuration["Auth:JWTSigningKey"],
                expireAt: accessTokenExpiryTime,
                privileges: u =>
                {
                    u.Roles.Add(user.Role.ToString());
                    
                    u.Permissions.AddRange(new[] { "Browse" });
                    
                    u.Claims.Add(new("userName", user.UserName));
                    u.Claims.Add(new(ClaimTypes.Email, user.Email));
                    
                    u["userId"] = user.Id; //indexer based claim setting
                });
            
            if (command.RememberMe)
            {
                var refreshToken = AuthTokenHelper.GenerateRefreshToken();
                loginUserResponse.AuthTokensResponse.RefreshToken = refreshToken.RefreshToken;
                loginUserResponse.AuthTokensResponse.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
                user.RefreshToken = refreshToken.RefreshToken;
                user.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
                _dbContext.Set<Domain.Aggregates.UserAggregate.User>().Update(user);
            }
            
            loginUserResponse.AuthTokensResponse.AccessToken = jwtToken;
            loginUserResponse.AuthTokensResponse.AccessTokenExpiryTime = accessTokenExpiryTime;
            
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            if (result.IsError)
                return result.Errors;
            
            return loginUserResponse;
        }
        catch (Exception ex)
        {
            Log.Error("Didn't manage to login user: {email} : {ex}",command.Email,ex);
            return Error.Unexpected(description: $"Didn't manage to login user: {command.Email}");
        }
    }
    
    private ErrorOr<bool> CheckPassword(string password,
        string passwordHash)
    {
        var passwordHelper = new PasswordHelper(Convert.FromBase64String(_configuration["Auth:Pepper"]));
        
        if (!passwordHelper.VerifyPassword(password, passwordHash))
        {
            return Error.Failure(description: "Credentials don't match.");
        }

        return true;
    }
}