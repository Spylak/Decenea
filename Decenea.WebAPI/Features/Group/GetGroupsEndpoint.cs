using Decenea.Application.Groups.Queries.GetGroups;
using Decenea.Application.Users.Queries.GetManyUsers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Enums;
using Decenea.Domain.Aggregates.UserAggregate;

namespace Decenea.WebAPI.Features.Group;

public class GetGroupsEndpoint : Endpoint<EmptyRequest, ApiResponse<List<GroupDto>>>
{
    public override void Configure()
    {
        Get("/groups/get-many");
        Roles(UserRoleExtensions.GetAuthorizesRoles());
    }

    public override async Task<ApiResponse<List<GroupDto>>> ExecuteAsync(EmptyRequest req, CancellationToken ct)
    {
        var result = await new GetGroupsQuery()
        {
            UserEmail = HttpContext.User.FindFirst("email")?.Value ?? ""
        }.ExecuteAsync(ct);
        return new ApiResponse<List<GroupDto>>(result.SuccessValue , result.IsSuccess, result.Messages);
    }
}