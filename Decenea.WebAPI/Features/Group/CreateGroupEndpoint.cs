using Decenea.Application.Groups.Commands.CreateGroup;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Features.Group;

public class CreateGroupEndpoint : Endpoint<CreateGroupRequest, ApiResponseResult<GroupDto>>
{
    public override void Configure()
    {
        Post("/groups/create");
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<GroupDto>> ExecuteAsync(CreateGroupRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");
        
        if(userId is null)
            return new ApiResponseResult<GroupDto>(null, false, "Invalid JWT.");
        
        var result = await new CreateGroupCommand()
        {
            UserId = userId,
            Name = req.Name
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<GroupDto>(result.Value, result.IsError, result.Errors.ToErrorDictionary());
    }
}