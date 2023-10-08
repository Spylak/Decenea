using System.Security.Cryptography;
using Decenea.Common.DataTransferObjects.Auth;

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
}