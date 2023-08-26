using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Decenea.Domain.Common;
using Decenea.Shared.DataTransferObjects.Auth;

namespace Decenea.Domain.Helpers;

public static  class AuthTokenHelper
{
    public static RefreshTokenDto GenerateRefreshToken(int numberOfBytes = 64, DateTime? refreshTokenExpiryTime = null)
    {
        refreshTokenExpiryTime ??= DateTime.UtcNow.AddDays(30);
        var randomBytes = RandomNumberGenerator.GetBytes(numberOfBytes);
        var refreshToken = Convert.ToBase64String(randomBytes);
        return new RefreshTokenDto(refreshToken, refreshTokenExpiryTime.Value);
    }
    
    public static Result<List<ClaimJwt>,Exception> GetTokenClaims(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                throw new ArgumentException("The given token cannot be read");
            }

            var jwtToken = handler.ReadJwtToken(token);

            var claims = new List<ClaimJwt>();
            foreach (var claim in jwtToken.Claims)
            {
                claims.Add(new ClaimJwt(claim.Type,claim.Value));
            }

            return Result<List<ClaimJwt>,Exception>.Anticipated(claims);
        }
        catch (Exception e)
        {
            return Result<List<ClaimJwt>,Exception>
                .Excepted(e,$"Didn't manage to get values from token: {token}");
        }
    }
}