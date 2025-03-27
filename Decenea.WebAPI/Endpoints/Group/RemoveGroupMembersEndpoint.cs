using System.Security.Claims;
using Decenea.Application.Features.Group.Commands.RemoveGroupMembers;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Endpoints.Group;

public class RemoveGroupMembersEndpoint : Endpoint<RemoveGroupMembersRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Put(RouteConstants.GroupsRemoveGroupMembers);
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<object>> ExecuteAsync(RemoveGroupMembersRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");
        var userEmail = claims.Value?.GetClaimValueByKey(ClaimTypes.Email);
        
        if(userId is null || userEmail is null)
            return new ApiResponseResult<object>(null, true, "Invalid JWT.");
        
        var result = await new RemoveGroupMembersCommand()
        {
            UserId = userId,
            UserEmail = userEmail,
            GroupId = req.GroupId,
            GroupUserEmails = req.GroupUserEmails
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<object>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}