using Decenea.Application.Features.User.Commands.RegenerateAuthTokens;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.User;


namespace Decenea.WebAPI.Endpoints.User;

public class RegenerateAuthTokensEndpoint : Endpoint<RegenerateAuthTokensRequest, ApiResponseResult<AuthTokensResponse>>
{
    public override void Configure()
    {
        Put(RouteConstants.UsersRegenerateAuthTokens);
        AllowAnonymous();
    }

    public override async Task<ApiResponseResult<AuthTokensResponse>> ExecuteAsync(RegenerateAuthTokensRequest req,
        CancellationToken ct)
    {
        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString().Replace("Bearer ","");
        var result = await new RegenerateAuthTokensCommand(accessToken, req.RefreshToken).ExecuteAsync(ct);
        return new ApiResponseResult<AuthTokensResponse>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}