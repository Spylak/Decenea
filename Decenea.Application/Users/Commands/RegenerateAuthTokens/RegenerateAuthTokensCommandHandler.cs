using System.Security.Claims;
using Decenea.Application.Abstractions.Persistance;
using ErrorOr;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.Common.Extensions;
using Decenea.Domain.Aggregates.UserAggregate;
using Decenea.Domain.Helpers;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Decenea.Application.Users.Commands.RegenerateAuthTokens;

public class RegenerateAuthTokensCommandHandler : ICommandHandler<RegenerateAuthTokensCommand, ErrorOr<RegenerateAuthTokensResponse>>
{
    private readonly IDeceneaDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public RegenerateAuthTokensCommandHandler(IDeceneaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<ErrorOr<RegenerateAuthTokensResponse>> ExecuteAsync(RegenerateAuthTokensCommand command,
        CancellationToken cancellationToken)
    {
        var claims = command.AccessToken.GetTokenClaimJwts();

        if (claims.IsError)
        {
            return claims.Errors;
        }

        var username = claims.Value?.GetUserNameClaimValue();

        var email = claims.Value?.GetEmailClaimValue();

        if (email is null)
        {
            return Error.NotFound(description: "Email not found.");
        }

        try
        {
            var user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(i => i.Email == email, cancellationToken);

            if (user is null)
                return Error.NotFound(description: "User not found.");

            if (user.LockoutEnabled)
                return Error.Forbidden(description: "User is locked.");

            if (user.RefreshToken != command.RefreshToken)
                return Error.Unauthorized(description: "Invalid refresh token.");

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
                return Error.Failure(description: "Expired refresh token.");
            
            var accessTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            var jwtToken = JWTBearer.CreateToken(
                signingKey: _configuration["Auth:JWTSigningKey"],
                expireAt: accessTokenExpiryTime,
                privileges: u =>
                {
                    u.Roles.Add(nameof(user.Role));

                    u.Permissions.AddRange(new[] { "Browse", "Edit" });

                    u.Claims.Add(new("userName", username));
                    u.Claims.Add(new(ClaimTypes.Email, email));

                    u["userId"] = user.Id; //indexer based claim setting
                });

            var refreshToken = AuthTokenHelper.GenerateRefreshToken();
            user.RefreshToken = refreshToken.RefreshToken;
            user.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            if (result.IsError)
                return result.Errors;

            return new RegenerateAuthTokensResponse(jwtToken, refreshToken.RefreshToken,
                    refreshToken.RefreshTokenExpiryTime, accessTokenExpiryTime);
        }
        catch (Exception e)
        {
            Log.Error("Failed to RegenerateAuthTokens for {user}, with error : {ex}", email, e);
            return Error.Unexpected(description: $"Didn't manage to RegenerateAuthTokens user: {email}");
        }
    }
}