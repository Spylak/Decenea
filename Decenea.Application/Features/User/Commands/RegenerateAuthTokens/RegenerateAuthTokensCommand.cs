using Decenea.Common.DataTransferObjects.Auth;
using FastEndpoints;

namespace Decenea.Application.Features.User.Commands.RegenerateAuthTokens;

public record RegenerateAuthTokensCommand(string AccessToken, string RefreshToken) : ICommand<ErrorOr<AuthTokensResponse>>;