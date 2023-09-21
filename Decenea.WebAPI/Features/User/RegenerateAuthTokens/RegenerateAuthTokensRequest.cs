namespace Decenea.WebAPI.Features.User.RegenerateAuthTokens;

public record RegenerateAuthTokensRequest(string AccessToken, string RefreshToken);