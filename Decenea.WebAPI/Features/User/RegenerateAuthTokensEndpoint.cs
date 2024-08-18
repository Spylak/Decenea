using Decenea.Application.Users.Commands.RegenerateAuthTokens;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.Domain.Aggregates.UserAggregate;


namespace Decenea.WebAPI.Features.User;

public class RegenerateAuthTokensEndpoint : Endpoint<EmptyRequest, ApiResponse<RegenerateAuthTokensResponse>>
{
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
        var result = await new RegenerateAuthTokensCommand(accessToken, refreshToken).ExecuteAsync(ct);
        return new ApiResponse<RegenerateAuthTokensResponse>(result.SuccessValue, result.IsSuccess, result.Messages);
    }
}