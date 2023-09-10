using Decenea.Domain.Common;
using Mediator;

namespace Decenea.Application.Advertisements.Commands.LoginUser;

public record LoginUserCommand(string Email,
string Password,
bool RememberMe) : ICommand<Result<LoginUserResponse, Exception>>;