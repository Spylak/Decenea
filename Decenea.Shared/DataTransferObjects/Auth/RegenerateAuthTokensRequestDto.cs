namespace Decenea.Shared.DataTransferObjects.Auth;

public record RegenerateAuthTokensRequestDto(string AccessToken, string RefreshToken);