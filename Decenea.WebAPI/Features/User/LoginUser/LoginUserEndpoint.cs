using Decenea.Application.Advertisements.Commands.LoginUser;
using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Shared.Common;
using Decenea.Shared.DataTransferObjects.ApplicationUser;
using Mediator;

namespace Decenea.WebAPI.Features.User.LoginUser;

public class LoginUserEndpoint : Endpoint<LoginUserRequest, ApiResponse<LoginUserResponse>>
{
    private readonly IMediator _mediator;
    public LoginUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put("/ApplicationUser/Login");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<LoginUserResponse>> ExecuteAsync(LoginUserRequest req,
        CancellationToken ct)
    {
        var command = new LoginUserCommand(req.Email,req.Password,req.RememberMe ?? false);
        var result = await _mediator.Send(command);
        return new ApiResponse<LoginUserResponse>(result.Value, result.IsSuccess, result.Messages);
    }
}