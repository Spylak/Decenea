using System.Security.Claims;
using Decenea.Application.Features.Group.Queries.GetGroup;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Endpoints.Group;

public class GetGroupEndpoint : Endpoint<GetGroupRequest, ApiResponseResult<GroupDto>>
{
    public override void Configure()
    {
        Get(RouteConstants.GroupsGet);
        Roles(UserRoleExtensions.GetAuthorizedRoles());
    }

    public override async Task<ApiResponseResult<GroupDto>> ExecuteAsync(GetGroupRequest req, CancellationToken ct)
    {
        var result = await new GetGroupQuery()
        {
            UserEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? "",
            GroupId = req.Id
        }.ExecuteAsync(ct);
        return new ApiResponseResult<GroupDto>(result.Value , result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}