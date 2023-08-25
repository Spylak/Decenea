using Decenea.WebAPI.Services.CommandServices.ICommandServices;
using Decenea.WebAPI.Domain.Common;
using Decenea.Domain.DataTransferObjects.ApplicationUser;
using Decenea.Domain.DataTransferObjects.Auth;

namespace Decenea.WebAPI.Features.ApplicationUser;

public class RegenerateAuthTokens : Endpoint<EmptyRequest, ApiResponse<RegenerateAuthTokensResponseDto>>
{
    private readonly IApplicationUserCommandService _applicationUserCommandService;

    public RegenerateAuthTokens(IApplicationUserCommandService applicationUserCommandService)
    {
        _applicationUserCommandService = applicationUserCommandService;
    }

    public override void Configure()
    {
        Put("/ApplicationUser/RegenerateAuthTokens");
    }

    public override async Task<ApiResponse<RegenerateAuthTokensResponseDto>> ExecuteAsync(EmptyRequest emptyRequest,
        CancellationToken ct)
    {
        var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","");
        var refreshToken = HttpContext.Request.Headers["RefreshToken"].ToString();
        var regenerateAuthTokensRequestDto = new RegenerateAuthTokensRequestDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        var result = await _applicationUserCommandService.RegenerateAuthTokens(regenerateAuthTokensRequestDto);
        return new ApiResponse<RegenerateAuthTokensResponseDto>(result.Value, result.IsSuccess, result.Message);
    }
}