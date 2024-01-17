namespace Decenea.Common.Requests.User;

public record RegenerateAuthTokensRequest(string AccessToken, string RefreshToken);