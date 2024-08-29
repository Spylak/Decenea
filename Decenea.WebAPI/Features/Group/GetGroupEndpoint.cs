using Decenea.Application.Groups.Queries.GetGroup;
using Decenea.Application.Groups.Queries.GetGroups;
using Decenea.Application.Users.Queries.GetManyUsers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Enums;
using Decenea.Common.Requests.Group;
using Decenea.Domain.Aggregates.UserAggregate;

namespace Decenea.WebAPI.Features.Group;

public class GetGroupEndpoint : Endpoint<GetGroupRequest, ApiResponse<GroupDto>>
{
    public override void Configure()
    {
        Get("/groups/get");
        Roles(UserRoleExtensions.GetAuthorizesRoles());
    }

    public override async Task<ApiResponse<GroupDto>> ExecuteAsync(GetGroupRequest req, CancellationToken ct)
    {
        var result = await new GetGroupQuery()
        {
            UserEmail = HttpContext.User.FindFirst("email")?.Value ?? "",
            GroupId = req.Id
        }.ExecuteAsync(ct);
        return new ApiResponse<GroupDto>(result.SuccessValue , result.IsSuccess, result.Messages);
    }
}