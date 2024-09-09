using System.Security.Claims;
using Decenea.Application.Groups.Commands.DeleteGroup;
using Decenea.Common.Common;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Features.Group;

public class DeleteGroupEndpoint : Endpoint<DeleteGroupRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Delete("/groups/delete");
        Roles(UserRoleExtensions.GetAuthorizesRoles());
    }

    public override async Task<ApiResponseResult<object>> ExecuteAsync(DeleteGroupRequest req, CancellationToken ct)
    {
        var result = await new DeleteGroupCommand()
        {
            UserEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? "",
            GroupId = req.Id
        }.ExecuteAsync(ct);
        return new ApiResponseResult<object>(result.Value, result.IsError, result.Errors.ToErrorDictionary());
    }
}