using Decenea.Application.Users.Commands.RegenerateAuthTokens;
using Decenea.Common.Common;
using Decenea.Domain.Aggregates.UserAggregate;
using Mediator;

namespace Decenea.WebAPI.Features.User;

public class RegenerateAuthTokensEndpoint : Endpoint<EmptyRequest, ApiResponse<RegenerateAuthTokensResponse>>
{
    private readonly IMediator _mediator;
    public RegenerateAuthTokensEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put("/User/RegenerateAuthTokens");
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