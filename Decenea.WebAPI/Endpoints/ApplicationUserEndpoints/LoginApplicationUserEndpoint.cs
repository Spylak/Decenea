using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Domain.Common;
using Decenea.Domain.DataTransferObjects.ApplicationUser.LoginApplicationUser;

namespace Decenea.WebAPI.Endpoints.ApplicationUserEndpoints;

public class LoginApplicationUserEndpoint : Endpoint<LoginApplicationUserRequest, ApiResponse<LoginApplicationUserDto>>
{
    private readonly IApplicationUserCommandService _applicationUserCommandService;

    public LoginApplicationUserEndpoint(IApplicationUserCommandService applicationUserCommandService)
    {
        _applicationUserCommandService = applicationUserCommandService;
    }

    public override void Configure()
    {
        Put("/ApplicationUser/Login");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<LoginApplicationUserDto>> ExecuteAsync(LoginApplicationUserRequest req,
        CancellationToken ct)
    {
        var result = await _applicationUserCommandService.LoginUser(req);
        return new ApiResponse<LoginApplicationUserDto>(result.Value, result.IsSuccess, result.Message);
    }
}