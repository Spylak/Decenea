using Decenea.Application.Features.Group.Commands.RemoveGroupMembers;
using Decenea.Common.Common;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Endpoints.Group;

public class RemoveGroupMembersEndpoint : Endpoint<RemoveGroupMembersRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Put("/groups/remove-members");
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
        var email = claims.Value?.GetClaimValueByKey("email");
        
        if(userId is null || email is null)
            return new ApiResponseResult<object>(null, true, "Invalid JWT.");
        
        var result = await new RemoveGroupMembersCommand()
        {
            UserId = userId,
            UserEmail = email,
            GroupId = req.GroupId,
            GroupUserEmails = req.GroupUserEmails
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<object>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}