using Decenea.Common.Common;
using Mediator;

namespace Decenea.Application.Users.Commands.RegenerateAuthTokens;

public record RegenerateAuthTokensCommand(string AccessToken, string RefreshToken) : ICommand<Result<RegenerateAuthTokensResponse,Exception>>
{
    
}