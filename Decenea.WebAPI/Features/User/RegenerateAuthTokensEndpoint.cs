using Decenea.Application.Users.Commands.RegenerateAuthTokens;
using Decenea.Common.Common;
using Decenea.Domain.Aggregates.UserAggregate;


namespace Decenea.WebAPI.Features.User;

public class RegenerateAuthTokensEndpoint : Endpoint<EmptyRequest, ApiResponse<RegenerateAuthTokensResponse>>
{
    private readonly RegenerateAuthTokensCommandHandler _regenerateAuthTokensCommandHandler;
    public RegenerateAuthTokensEndpoint(RegenerateAuthTokensCommandHandler regenerateAuthTokensCommandHandler)
    {
        _regenerateAuthTokensCommandHandler = regenerateAuthTokensCommandHandler;
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
        var result = await _regenerateAuthTokensCommandHandler.Handle(regenerateAuthTokensRequestDto, ct);
        return new ApiResponse<RegenerateAuthTokensResponse>(result.Value, result.IsSuccess, result.Messages);
    }
}