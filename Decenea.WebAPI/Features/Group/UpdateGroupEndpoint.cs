using Decenea.Application.Groups.Commands.CreateGroup;
using Decenea.Application.Groups.Commands.UpdateGroup;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;
using Decenea.Domain.Aggregates.UserAggregate;

namespace Decenea.WebAPI.Features.Group;

public class UpdateGroupEndpoint : Endpoint<UpdateGroupRequest, ApiResponse<GroupDto>>
{
    public override void Configure()
    {
        Put("/groups/update");
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponse<GroupDto>> ExecuteAsync(UpdateGroupRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.SuccessValue?.GetClaimValueByKey("userId");
        
        if(userId is null)
            return new ApiResponse<GroupDto>(null, false, "Invalid JWT.");
        
        var result = await new UpdateGroupCommand()
        {
            UserId = userId,
            GroupId = req.Id,
            Name = req.Name
        }.ExecuteAsync(ct);
        
        return new ApiResponse<GroupDto>(result.SuccessValue, result.IsSuccess, result.Messages);
    }
}