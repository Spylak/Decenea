using Decenea.Common.DataTransferObjects.Auth;
using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Features.User.Commands.LoginUser;

public record LoginUserCommand(string Email,
string Password,
bool RememberMe) : ICommand<ErrorOr<LoginUserResponse>>;