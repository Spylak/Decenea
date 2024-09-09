using Decenea.Application.Groups.Commands.UpdateGroup;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Features.Group;

public class UpdateGroupEndpoint : Endpoint<UpdateGroupRequest, ApiResponseResult<GroupDto>>
{
    public override void Configure()
    {
        Put("/groups/update");
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<GroupDto>> ExecuteAsync(UpdateGroupRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");
        
        if(userId is null)
            return new ApiResponseResult<GroupDto>(null, false, "Invalid JWT.");
        
        var result = await new UpdateGroupCommand()
        {
            UserId = userId,
            GroupId = req.Id,
            Name = req.Name,
            Version = req.Version
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<GroupDto>(result.Value, result.IsError, result.Errors.ToErrorDictionary());
    }
}