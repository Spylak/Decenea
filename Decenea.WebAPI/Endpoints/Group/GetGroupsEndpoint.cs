using System.Security.Claims;
using Decenea.Application.Features.Group.Queries.GetGroups;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;

namespace Decenea.WebAPI.Endpoints.Group;

public class GetGroupsEndpoint : Endpoint<EmptyRequest, ApiResponseResult<List<GroupDto>>>
{
    public override void Configure()
    {
        Get(RouteConstants.GroupsGetMany);
        Roles(UserRoleExtensions.GetAuthorizedRoles());
    }

    public override async Task<ApiResponseResult<List<GroupDto>>> ExecuteAsync(EmptyRequest req, CancellationToken ct)
    {
        var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        
        if(userEmail is null)
            return new ApiResponseResult<List<GroupDto>>(null, true, "Invalid JWT.");
        
        var result = await new GetGroupsQuery()
        {
            UserEmail = userEmail
        }.ExecuteAsync(ct);
        return new ApiResponseResult<List<GroupDto>>(result.Value , result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}