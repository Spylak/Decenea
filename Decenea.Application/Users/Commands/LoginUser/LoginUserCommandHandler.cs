using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Helpers;
using FastEndpoints.Security;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Users.Commands.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand,Result<LoginUserResponse,Exception>>
{
    private readonly IDeceneaDbContext _dbContext;
    public LoginUserCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async ValueTask<Result<LoginUserResponse, Exception>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var loginUserResponse = new LoginUserResponse();
            
            var user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(i=> i.Email == command.Email, cancellationToken);
            
            if(user is null)
                return Result<LoginUserResponse, Exception>.Anticipated(null,"User not found.");

            if(user.LockoutEnabled)
                return Result<LoginUserResponse, Exception>.Anticipated(null,"User is locked.");

            var passCheck = User.CheckPassword(command.Password,user.PasswordHash, user.PasswordSalt);
            
            if (!passCheck.IsSuccess)
            {
                return Result<LoginUserResponse, Exception>.Anticipated(null, passCheck.Messages);
            }

            var accessTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            var jwtToken = JWTBearer.CreateToken(
                signingKey: "ApplicationTokenSigningKey",
                expireAt: accessTokenExpiryTime,
                privileges: u =>
                {
                    u.Roles.Add(Role.RoleName(user.RoleId));
                    
                    u.Permissions.AddRange(new[] { "Browse" });
                    
                    u.Claims.Add(new("userName", user.UserName));
                    u.Claims.Add(new("email", user.Email));
                    
                    u["userId"] = user.Id; //indexer based claim setting
                    u["cityId"] = user.CityId; 
                });
            
            if (command.RememberMe)
            {
                var refreshToken = AuthTokenHelper.GenerateRefreshToken();
                loginUserResponse.RefreshToken = refreshToken.RefreshToken;
                loginUserResponse.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
                user.RefreshToken = refreshToken.RefreshToken;
                user.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
                _dbContext.Set<User>().Update(user);
            }
            
            loginUserResponse.CityId = user.CityId;
            loginUserResponse.AccessToken = jwtToken;
            loginUserResponse.AccessTokenExpiryTime = accessTokenExpiryTime;
            user.ApplicationUserToLoginApplicationUserDto(loginUserResponse);
            
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            
            if (!result.IsSuccess)
                return Result<LoginUserResponse, Exception>.Anticipated(null, result.Messages);
            
            return Result<LoginUserResponse, Exception>.Anticipated(loginUserResponse);
        }
        catch (Exception ex)
        {
            Log.Error("Didn't manage to login user: {email} : {ex}",command.Email,ex);
            return Result<LoginUserResponse, Exception>
                .Excepted(ex,$"Didn't manage to login user: {command.Email}");
        }
    }
}