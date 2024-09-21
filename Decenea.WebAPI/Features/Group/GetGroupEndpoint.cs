using System.Security.Claims;
using Decenea.Application.Groups.Queries.GetGroup;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Features.Group;

public class GetGroupEndpoint : Endpoint<GetGroupRequest, ApiResponseResult<GroupDto>>
{
    public override void Configure()
    {
        Get("/groups/get");
        Roles(UserRoleExtensions.GetAuthorizesRoles());
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