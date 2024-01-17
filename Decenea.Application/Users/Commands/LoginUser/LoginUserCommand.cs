using Decenea.Common.Common;


namespace Decenea.Application.Users.Commands.LoginUser;

public record LoginUserCommand(string Email,
string Password,
bool RememberMe);