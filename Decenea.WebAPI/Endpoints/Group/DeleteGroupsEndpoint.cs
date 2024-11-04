using System.Security.Claims;
using Decenea.Application.Features.Group.Commands.DeleteGroups;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Group;

namespace Decenea.WebAPI.Endpoints.Group;

public class DeleteGroupsEndpoint : Endpoint<DeleteGroupsRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Delete(RouteConstants.GroupsDelete);
        Roles(UserRoleExtensions.GetAuthorizedRoles());
    }

    public override async Task<ApiResponseResult<object>> ExecuteAsync(DeleteGroupsRequest req, CancellationToken ct)
    {
        var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        
        if(userEmail is null)
            return new ApiResponseResult<object>(null, true, "Invalid JWT.");
        
        var result = await new DeleteGroupsCommand()
        {
            UserEmail = userEmail,
            GroupIds = req.Ids
        }.ExecuteAsync(ct);
        return new ApiResponseResult<object>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}