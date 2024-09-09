using ErrorOr;
using Decenea.Common.DataTransferObjects.Auth;
using FastEndpoints;


namespace Decenea.Application.Users.Commands.LoginUser;

public record LoginUserCommand(string Email,
string Password,
bool RememberMe) : ICommand<ErrorOr<LoginUserResponse>>;