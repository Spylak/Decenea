using Decenea.Application.Users.Commands.RegenerateAuthTokens;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.Domain.Aggregates.UserAggregate;
using Mediator;

namespace Decenea.WebAPI.Features.User.RegenerateAuthTokens;

public class RegenerateAuthTokens : Endpoint<EmptyRequest, ApiResponse<RegenerateAuthTokensResponse>>
{
    private readonly IMediator _mediator;
    public RegenerateAuthTokens(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put("/ApplicationUser/RegenerateAuthTokens");
        Roles(Role.AllowAny());
    }

    public override async Task<ApiResponse<RegenerateAuthTokensResponse>> ExecuteAsync(EmptyRequest emptyRequest,
        CancellationToken ct)
    {
        var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ","");
        var refreshToken = HttpContext.Request.Headers["RefreshToken"].ToString();
        var regenerateAuthTokensRequestDto = new RegenerateAuthTokensCommand(accessToken, refreshToken);
        var result = await _mediator.Send(regenerateAuthTokensRequestDto);
        return new ApiResponse<RegenerateAuthTokensResponse>(result.Value, result.IsSuccess, result.Messages);
    }
}