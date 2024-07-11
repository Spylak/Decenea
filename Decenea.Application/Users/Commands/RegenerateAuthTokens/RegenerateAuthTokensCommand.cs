using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Auth;
using FastEndpoints;


namespace Decenea.Application.Users.Commands.RegenerateAuthTokens;

public record RegenerateAuthTokensCommand(string AccessToken, string RefreshToken) : ICommand<Result<RegenerateAuthTokensResponse, Exception>>;