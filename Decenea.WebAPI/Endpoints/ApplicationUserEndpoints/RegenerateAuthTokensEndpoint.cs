using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Domain.Common;
using Decenea.Domain.DataTransferObjects.ApplicationUser;
using Decenea.Domain.DataTransferObjects.Auth;

namespace Decenea.WebAPI.Endpoints.ApplicationUserEndpoints;

public class RegenerateAuthTokensEndpoint : Endpoint<RegenerateAuthTokensRequestDto, ApiResponse<RegenerateAuthTokensResponseDto>>
{
    private readonly IApplicationUserCommandService _applicationUserCommandService;

    public RegenerateAuthTokensEndpoint(IApplicationUserCommandService applicationUserCommandService)
    {
        _applicationUserCommandService = applicationUserCommandService;
    }

    public override void Configure()
    {
        Put("/ApplicationUser/RegenerateAuthTokens");
        Claims();
    }

    public override async Task<ApiResponse<RegenerateAuthTokensResponseDto>> ExecuteAsync(RegenerateAuthTokensRequestDto req,
        CancellationToken ct)
    {
        var result = await _applicationUserCommandService.RegenerateAuthTokens(req);
        return new ApiResponse<RegenerateAuthTokensResponseDto>(result.Value, result.IsSuccess, result.Message);
    }
}