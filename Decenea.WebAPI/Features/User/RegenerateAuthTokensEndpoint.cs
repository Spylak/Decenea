using Decenea.Application.Users.Commands.RegenerateAuthTokens;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.User;


namespace Decenea.WebAPI.Features.User;

public class RegenerateAuthTokensEndpoint : Endpoint<RegenerateAuthTokensRequest, ApiResponseResult<RegenerateAuthTokensResponse>>
{
    public override void Configure()
    {
        Put("/auth/regenerate-auth-tokens");
        Roles(EnumExtensions.GetNames<UserRole>());
    }

    public override async Task<ApiResponseResult<RegenerateAuthTokensResponse>> ExecuteAsync(RegenerateAuthTokensRequest req,
        CancellationToken ct)
    {
        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString().Replace("Bearer ","");
        var result = await new RegenerateAuthTokensCommand(accessToken, req.RefreshToken).ExecuteAsync(ct);
        return new ApiResponseResult<RegenerateAuthTokensResponse>(result.Value, result.IsError, result.Errors.ToErrorDictionary());
    }
}