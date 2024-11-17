using System.Security.Claims;
using Decenea.Application.Features.Group.Commands.AddGroupMembers;
using Decenea.Application.Features.Group.Commands.SyncTests;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Endpoints.Group;

public class SyncTestsEndpoint : Endpoint<SyncTestsRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Put(RouteConstants.GroupsSyncTests);
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<object>> ExecuteAsync(SyncTestsRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");
        
        var userEmail = claims.Value?.GetClaimValueByKey(ClaimTypes.Email);
        
        if(userId is null || userEmail is null)
            return new ApiResponseResult<object>(null, true, "Invalid JWT.");

        var result = await new SyncTestsCommand()
        {
            UserId = userId,
            GroupId = req.GroupId,
            TestIds = req.TestIds,
            UserEmail = userEmail
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<object>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}