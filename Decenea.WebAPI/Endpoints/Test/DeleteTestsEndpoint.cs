using Decenea.Application.Features.Test.Commands.DeleteTests;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Test;

namespace Decenea.WebAPI.Endpoints.Test;

public class DeleteTestsEndpoint : Endpoint<DeleteTestsRequest, ApiResponseResult<object>>
{
    public override void Configure()
    {
        Delete(RouteConstants.TestsDelete);
        Roles(UserRoleExtensions.GetAuthorizedRoles());
    }

    public override async Task<ApiResponseResult<object>> ExecuteAsync(DeleteTestsRequest req, CancellationToken ct)
    {
        var result = await new DeleteTestsCommand
        {
            UserId = HttpContext.User.FindFirst("userId")?.Value ?? "",
            TestIds = req.Ids
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<object>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}