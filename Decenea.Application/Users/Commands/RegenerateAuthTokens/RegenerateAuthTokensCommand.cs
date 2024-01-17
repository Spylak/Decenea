using Decenea.Common.Common;


namespace Decenea.Application.Users.Commands.RegenerateAuthTokens;

public record RegenerateAuthTokensCommand(string AccessToken, string RefreshToken);