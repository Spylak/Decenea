namespace Decenea.Shared.DataTransferObjects.Auth;

public record RegenerateAuthTokensResponseDto(string AccessToken, string RefreshToken, DateTime RefreshTokenExpiryTime, DateTime AccessTokenExpiryTime);