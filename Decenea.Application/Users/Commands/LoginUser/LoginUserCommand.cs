using Decenea.Common.Common;
using Mediator;

namespace Decenea.Application.Users.Commands.LoginUser;

public record LoginUserCommand(string Email,
string Password,
bool RememberMe) : ICommand<Result<LoginUserResponse, Exception>>;