using Decenea.Application.Groups.Commands.AddGroupMember;
using Decenea.Common.Common;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Features.Group;

public class AddGroupMemberEndpoint : Endpoint<AddGroupMemberRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Put("/groups/add-member");
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<object>> ExecuteAsync(AddGroupMemberRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");
        
        if(userId is null)
            return new ApiResponseResult<object>(null, false, "Invalid JWT.");
        
        var result = await new AddGroupMemberCommand()
        {
            UserId = userId,
            GroupId = req.GroupId,
            GroupUserEmail = req.GroupUserEmail,
            GroupRole = req.GroupRole
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<object>(result.Value, result.IsError, result.Errors.ToErrorDictionary());
    }
}