namespace Decenea.Common.Requests.Users;

public record RegenerateAuthTokensRequest(string AccessToken, string RefreshToken);