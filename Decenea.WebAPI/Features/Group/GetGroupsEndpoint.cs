using System.Security.Claims;
using Decenea.Application.Groups.Queries.GetGroups;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;

namespace Decenea.WebAPI.Features.Group;

public class GetGroupsEndpoint : Endpoint<EmptyRequest, ApiResponseResult<List<GroupDto>>>
{
    public override void Configure()
    {
        Get("/groups/get-many");
        Roles(UserRoleExtensions.GetAuthorizesRoles());
    }

    public override async Task<ApiResponseResult<List<GroupDto>>> ExecuteAsync(EmptyRequest req, CancellationToken ct)
    {
        var result = await new GetGroupsQuery()
        {
            UserEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? ""
        }.ExecuteAsync(ct);
        return new ApiResponseResult<List<GroupDto>>(result.Value , result.IsError, result.Errors.ToErrorDictionary());
    }
}