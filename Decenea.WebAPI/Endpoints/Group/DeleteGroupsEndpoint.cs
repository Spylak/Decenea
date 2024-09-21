using System.Security.Claims;
using Decenea.Application.Features.Group.Commands.DeleteGroups;
using Decenea.Common.Common;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Endpoints.Group;

public class DeleteGroupsEndpoint : Endpoint<DeleteGroupsRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Delete("/groups/delete");
        Roles(UserRoleExtensions.GetAuthorizesRoles());
    }

    public override async Task<ApiResponseResult<object>> ExecuteAsync(DeleteGroupsRequest req, CancellationToken ct)
    {
        var result = await new DeleteGroupsCommand()
        {
            UserEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? "",
            GroupIds = req.Ids
        }.ExecuteAsync(ct);
        return new ApiResponseResult<object>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}