using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Users.Commands.LoginUser;
using Decenea.Common.Common;
using Decenea.Common.Requests.User;


namespace Decenea.WebAPI.Features.User;

public class LoginUserEndpoint : Endpoint<LoginUserRequest, ApiResponse<LoginUserResponse>>
{
    private readonly LoginUserCommandHandler _loginUserCommandHandler;
    public LoginUserEndpoint(LoginUserCommandHandler loginUserCommandHandler)
    {
        _loginUserCommandHandler = loginUserCommandHandler;
    }

    public override void Configure()
    {
        Put("/User/Login");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<LoginUserResponse>> ExecuteAsync(LoginUserRequest req,
        CancellationToken ct)
    {
        var command = new LoginUserCommand(req.Email,req.Password,req.RememberMe ?? false);
        var result = await _loginUserCommandHandler.Handle(command, ct);
        return new ApiResponse<LoginUserResponse>(result.Value, result.IsSuccess, result.Messages);
    }
}