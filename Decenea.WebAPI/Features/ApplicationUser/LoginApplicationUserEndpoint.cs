using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Shared.Common;
using Decenea.Shared.DataTransferObjects.ApplicationUser;

namespace Decenea.WebAPI.Features.ApplicationUser;

public class LoginApplicationUser : Endpoint<LoginApplicationUserRequestDto, ApiResponse<LoginApplicationUserResponseDto>>
{
    private readonly IApplicationUserCommandService _applicationUserCommandService;

    public LoginApplicationUser(IApplicationUserCommandService applicationUserCommandService)
    {
        _applicationUserCommandService = applicationUserCommandService;
    }

    public override void Configure()
    {
        Put("/ApplicationUser/Login");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<LoginApplicationUserResponseDto>> ExecuteAsync(LoginApplicationUserRequestDto req,
        CancellationToken ct)
    {
        var result = await _applicationUserCommandService.LoginUser(req);
        return new ApiResponse<LoginApplicationUserResponseDto>(result.Value, result.IsSuccess, result.Messages);
    }
}