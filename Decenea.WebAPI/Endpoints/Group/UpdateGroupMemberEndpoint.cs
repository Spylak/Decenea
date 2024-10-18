using System.Security.Claims;
using Decenea.Application.Features.Group.Commands.UpdateGroupMember;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Endpoints.Group;

public class UpdateGroupMemberEndpoint : Endpoint<UpdateGroupMemberRequest, ApiResponseResult<GroupMemberDto>>
{
    public override void Configure()
    {
        Put(RouteConstants.GroupsUpdateGroupMember);
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<GroupMemberDto>> ExecuteAsync(UpdateGroupMemberRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");
        
        var userEmail = claims.Value?.GetClaimValueByKey(ClaimTypes.Email);
        
        if(userId is null || userEmail is null)
            return new ApiResponseResult<GroupMemberDto>(null, true, "Invalid JWT.");

        var result = await new UpdateGroupMemberCommand()
        {
            UserId = userId,
            GroupId = req.GroupId,
            UpdateGroupMemberDto = req.UpdateGroupMemberDto,
            UserEmail = userEmail
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<GroupMemberDto>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}