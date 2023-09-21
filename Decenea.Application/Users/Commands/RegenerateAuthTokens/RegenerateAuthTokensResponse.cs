namespace Decenea.Application.Users.Commands.RegenerateAuthTokens;

public record RegenerateAuthTokensResponse(string AccessToken, string RefreshToken, DateTime RefreshTokenExpiryTime, DateTime AccessTokenExpiryTime);