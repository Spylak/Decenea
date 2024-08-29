using Decenea.Application.Groups.Commands.DeleteGroup;
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

public class DeleteGroupEndpoint : Endpoint<DeleteGroupRequest, ApiResponse<object>>
{
    public override void Configure()
    {
        Delete("/groups/delete");
        Roles(UserRoleExtensions.GetAuthorizesRoles());
    }

    public override async Task<ApiResponse<object>> ExecuteAsync(DeleteGroupRequest req, CancellationToken ct)
    {
        var result = await new DeleteGroupCommand()
        {
            UserEmail = HttpContext.User.FindFirst("email")?.Value ?? "",
            GroupId = req.Id
        }.ExecuteAsync(ct);
        return new ApiResponse<object>(result.SuccessValue , result.IsSuccess, result.Messages);
    }
}