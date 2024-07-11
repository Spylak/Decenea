using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Auth;
using FastEndpoints;


namespace Decenea.Application.Users.Commands.LoginUser;

public record LoginUserCommand(string Email,
string Password,
bool RememberMe) : ICommand<Result<LoginUserResponse, Exception>>;