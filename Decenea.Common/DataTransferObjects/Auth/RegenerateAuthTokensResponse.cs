namespace Decenea.Common.DataTransferObjects.Auth;

public record RegenerateAuthTokensResponse(string AccessToken, string RefreshToken, DateTime RefreshTokenExpiryTime, DateTime AccessTokenExpiryTime);