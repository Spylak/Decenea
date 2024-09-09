using Decenea.Common.DataTransferObjects.Auth;
using ErrorOr;
using FastEndpoints;


namespace Decenea.Application.Users.Commands.RegenerateAuthTokens;

public record RegenerateAuthTokensCommand(string AccessToken, string RefreshToken) : ICommand<ErrorOr<RegenerateAuthTokensResponse>>;