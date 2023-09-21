using System.Text.Json;
using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Common;
using Decenea.Common.Extensions;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Helpers;
using FastEndpoints.Security;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Users.Commands.RegenerateAuthTokens;

public class RegenerateAuthTokensCommandHandler : ICommandHandler<RegenerateAuthTokensCommand,
    Result<RegenerateAuthTokensResponse, Exception>>
{
    private readonly IDeceneaDbContext _dbContext;
    public RegenerateAuthTokensCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Result<RegenerateAuthTokensResponse, Exception>> Handle(RegenerateAuthTokensCommand command,
        CancellationToken cancellationToken)
    {
        
        var claims = AuthTokenHelper
            .GetTokenClaims(command.AccessToken);

        if (claims.Value is null)
        {
            return Result<RegenerateAuthTokensResponse, Exception>
                .Excepted(claims.Exception);
        }

        var username = claims.Value
            .GetUserNameClaimValue();
            
        var email = claims.Value
            .GetEmailClaimValue();

        if (email is null)
        {
            return Result<RegenerateAuthTokensResponse, Exception>
                .Anticipated(null, "Email not found.");
        }
        
        try
        {
            var user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(i=> i.Email == email, cancellationToken);
            
            if (user is null)
                return Result<RegenerateAuthTokensResponse, Exception>.Anticipated(null,"User not found.");

            if(user.LockoutEnabled)
                return Result<RegenerateAuthTokensResponse, Exception>.Anticipated(null,"User is locked.");
            
            if (user.RefreshToken != command.RefreshToken)
                return Result<RegenerateAuthTokensResponse, Exception>.Anticipated(null, "Invalid refresh token.");

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
                return Result<RegenerateAuthTokensResponse, Exception>.Anticipated(null, "Expired refresh token.");

            var userRole = Role.RoleName(user.RoleId);

            var accessTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            var jwtToken = JWTBearer.CreateToken(
                signingKey: "ApplicationTokenSigningKey",
                expireAt: accessTokenExpiryTime,
                priviledges: u =>
                {
                    u.Roles.Add(userRole);

                    u.Permissions.AddRange(new[] { "Browse", "Edit" });

                    u.Claims.Add(new("userName", username));
                    u.Claims.Add(new("email", email));

                    u["userId"] = user.Id; //indexer based claim setting
                    u["cityId"] = user.CityId;
                });

            var refreshToken = AuthTokenHelper.GenerateRefreshToken();
            user.RefreshToken = refreshToken.RefreshToken;
            user.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;
            
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            
            if (result == 0)
                return Result<RegenerateAuthTokensResponse, Exception>.Anticipated(null,"Something went wrong.");

            return Result<RegenerateAuthTokensResponse, Exception>
                .Anticipated(new RegenerateAuthTokensResponse(jwtToken, refreshToken.RefreshToken,
                    refreshToken.RefreshTokenExpiryTime, accessTokenExpiryTime));
        }
        catch (Exception e)
        {
            Log.Error("Failed to RegenerateAuthTokens for {user}, with error : {ex}", email, e);
            return Result<RegenerateAuthTokensResponse, Exception>
                .Excepted(e, $"Didn't manage to RegenerateAuthTokens user: {email}");
        }
    }
}