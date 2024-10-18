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
        var result = await new GetGroupsQuery()
        {
            UserEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? ""
        }.ExecuteAsync(ct);
        return new ApiResponseResult<List<GroupDto>>(result.Value , result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}