using System.Text.Json;
using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Common;
using Decenea.Domain.Helpers;
using FastEndpoints.Security;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Advertisements.Commands.LoginUser;

public class LoginUserHandler : ICommandHandler<LoginUserCommand,Result<LoginUserResponse,Exception>>
{
    private readonly IDeceneaDbContext _dbContext;
    public LoginUserHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async ValueTask<Result<LoginUserResponse, Exception>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var loginUserResponse = new LoginUserResponse();
            
            var userStream = await _dbContext.Set<UserSourceEventStream>()
                .FirstOrDefaultAsync(i=> i.Snapshot.Contains($"\"Email\":\"{command.Email}\""), cancellationToken);
            
            if (userStream is null)
                return Result<LoginUserResponse, Exception>.Anticipated(null,"User not found.");

            var user = JsonSerializer.Deserialize<User>(userStream.Snapshot)!;
            
            if(user.LockoutEnabled)
                return Result<LoginUserResponse, Exception>.Anticipated(null,"User is locked.");

            var passHelper = new PassowordHelper();
            
            if (!passHelper.VerifyPassword(command.Password,user.PasswordHash, user.PasswordSalt))
            {
                return Result<LoginUserResponse, Exception>.Anticipated(null,"Credentials don't match.");
            }

            var accessTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            var jwtToken = JWTBearer.CreateToken(
                signingKey: "ApplicationTokenSigningKey",
                expireAt: accessTokenExpiryTime,
                priviledges: u =>
                {
                    u.Roles.Add(Role.RoleName(user.RoleId));
                    
                    u.Permissions.AddRange(new[] { "Browse" });
                    
                    u.Claims.Add(new("UserName", user.Email));
                    
                    u["UserId"] = user.Id; //indexer based claim setting
                    u["CityId"] = user.CityId; 
                });
            
            if (command.RememberMe)
            {
                var refreshToken = AuthTokenHelper.GenerateRefreshToken();
                loginUserResponse.RefreshToken = refreshToken.RefreshToken;
                loginUserResponse.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
                user.RefreshToken = refreshToken.RefreshToken;
                user.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
                
                var eventData = JsonSerializer.Serialize(new
                {
                    RefreshToken = refreshToken.RefreshToken,
                    RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime
                });

                var userSourceEvent = new UserSourceEvent(userStream.AggregateVersion + 1, eventData, user.Id)
                {
                    StreamId = userStream.Id
                };
                
                await _dbContext.Set<UserSourceEvent>().AddAsync(userSourceEvent);
                
                var serUser = JsonSerializer.Serialize(user);
                userStream.Snapshot = serUser;
                userStream.AggregateVersion += 1;
                userStream.SnapshotVersion += 1;
                _dbContext.Set<UserSourceEventStream>().Update(userStream);
            }
            
            loginUserResponse.CityId = user.CityId;
            loginUserResponse.AccessToken = jwtToken;
            loginUserResponse.AccessTokenExpiryTime = accessTokenExpiryTime;
            user.ApplicationUserToLoginApplicationUserDto(loginUserResponse);
            
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            
            if (result == 0)
                return Result<LoginUserResponse, Exception>.Anticipated(null,"Something went wrong.");
            
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