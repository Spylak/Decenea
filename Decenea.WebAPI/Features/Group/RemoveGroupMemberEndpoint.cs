using Decenea.Application.Groups.Commands.RemoveGroupMember;
using Decenea.Common.Common;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Features.Group;

public class RemoveGroupMemberEndpoint : Endpoint<RemoveGroupMemberRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Put("/groups/remove-member");
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<object>> ExecuteAsync(RemoveGroupMemberRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");
        var email = claims.Value?.GetClaimValueByKey("email");
        
        if(userId is null || email is null)
            return new ApiResponseResult<object>(null, false, "Invalid JWT.");
        
        var result = await new RemoveGroupMemberCommand()
        {
            UserId = userId,
            UserEmail = email,
            GroupId = req.GroupId,
            GroupUserEmail = req.GroupUserEmail
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<object>(result.Value, result.IsError, result.Errors.ToErrorDictionary());
    }
}