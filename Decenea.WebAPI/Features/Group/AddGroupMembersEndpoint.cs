using System.Security.Claims;
using Decenea.Application.Groups.Commands.AddGroupMembers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Features.Group;

public class AddGroupMembersEndpoint : Endpoint<AddGroupMembersRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Put("/groups/add-members");
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<object>> ExecuteAsync(AddGroupMembersRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");
        
        var userEmail = claims.Value?.GetClaimValueByKey(ClaimTypes.Email);
        
        if(userId is null || userEmail is null)
            return new ApiResponseResult<object>(null, true, "Invalid JWT.");

        var result = await new AddGroupMembersCommand()
        {
            UserId = userId,
            GroupId = req.GroupId,
            GroupMemberDtos = req.GroupMembers,
            UserEmail = userEmail
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<object>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}