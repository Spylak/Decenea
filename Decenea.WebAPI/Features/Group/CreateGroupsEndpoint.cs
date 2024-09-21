using System.Security.Claims;
using Decenea.Application.Groups.Commands.CreateGroups;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Features.Group;

public class CreateGroupsEndpoint : Endpoint<CreateGroupsRequest, ApiResponseResult<List<GroupDto>>>
{
    public override void Configure()
    {
        Post("/groups/create");
        Roles(nameof(UserRole.SuperAdmin),
            nameof(UserRole.Admin),
            nameof(UserRole.Member));
    }
    
    public override async Task<ApiResponseResult<List<GroupDto>>> ExecuteAsync(CreateGroupsRequest req, CancellationToken ct)
    {

        var accessToken = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ","");

        var claims = accessToken.GetTokenClaimJwts();

        var userId = claims.Value?.GetClaimValueByKey("userId");

        var userEmail = claims.Value?.GetClaimValueByKey(ClaimTypes.Email);
        
        if(userId is null || userEmail is null)
            return new ApiResponseResult<List<GroupDto>>(null, true, "Invalid JWT.");
        
        var result = await new CreateGroupsCommand()
        {
            UserId = userId,
            GroupDtos = req.Groups,
            UserEmail = userEmail
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<List<GroupDto>>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}