using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Common;
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

public class RegenerateAuthTokensCommandHandler : ICommandHandler<RegenerateAuthTokensCommand, Result<RegenerateAuthTokensResponse, Exception>>
{
    private readonly IDeceneaDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public RegenerateAuthTokensCommandHandler(IDeceneaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<Result<RegenerateAuthTokensResponse, Exception>> ExecuteAsync(RegenerateAuthTokensCommand command,
        CancellationToken cancellationToken)
    {
        var claims = command.AccessToken.GetTokenClaimJwts();

        if (claims.SuccessValue is null)
        {
            return Result<RegenerateAuthTokensResponse, Exception>
                .Excepted(claims.ErrorValue);
        }

        var username = claims.SuccessValue
            .GetUserNameClaimValue();

        var email = claims.SuccessValue
            .GetEmailClaimValue();

        if (email is null)
        {
            return Result<RegenerateAuthTokensResponse, Exception>
                .Anticipated(null, ["Email not found."]);
        }

        try
        {
            var user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(i => i.Email == email, cancellationToken);

            if (user is null)
                return Result<RegenerateAuthTokensResponse, Exception>.Anticipated(null, ["User not found."]);

            if (user.LockoutEnabled)
                return Result<RegenerateAuthTokensResponse, Exception>.Anticipated(null, ["User is locked."]);

            if (user.RefreshToken != command.RefreshToken)
                return Result<RegenerateAuthTokensResponse, Exception>.Anticipated(null, ["Invalid refresh token."]);

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
                return Result<RegenerateAuthTokensResponse, Exception>.Anticipated(null, ["Expired refresh token."]);

            var userRole = Role.RoleName(user.RoleId);

            var accessTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            var jwtToken = JWTBearer.CreateToken(
                signingKey: _configuration["Auth:JWTSigningKey"],
                expireAt: accessTokenExpiryTime,
                privileges: u =>
                {
                    u.Roles.Add(userRole);

                    u.Permissions.AddRange(new[] { "Browse", "Edit" });

                    u.Claims.Add(new("userName", username));
                    u.Claims.Add(new("email", email));

                    u["userId"] = user.Id; //indexer based claim setting
                });

            var refreshToken = AuthTokenHelper.GenerateRefreshToken();
            user.RefreshToken = refreshToken.RefreshToken;
            user.RefreshTokenExpiryTime = refreshToken.RefreshTokenExpiryTime;

            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            if (!result.IsSuccess)
                return Result<RegenerateAuthTokensResponse, Exception>.Anticipated(null, result.Messages);

            return Result<RegenerateAuthTokensResponse, Exception>
                .Anticipated(new RegenerateAuthTokensResponse(jwtToken, refreshToken.RefreshToken,
                    refreshToken.RefreshTokenExpiryTime, accessTokenExpiryTime));
        }
        catch (Exception e)
        {
            Log.Error("Failed to RegenerateAuthTokens for {user}, with error : {ex}", email, e);
            return Result<RegenerateAuthTokensResponse, Exception>
                .Excepted(e, [$"Didn't manage to RegenerateAuthTokens user: {email}"]);
        }
    }
}