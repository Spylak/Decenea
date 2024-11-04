using System.Security.Claims;
using Decenea.Application.Features.Test.Queries.GetActiveTest;
using Decenea.Application.Features.Test.Queries.GetTest;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Test;


namespace Decenea.WebAPI.Endpoints.Test;

public class GetActiveTestEndpoint : Endpoint<GetActiveTestRequest, ApiResponseResult<ActiveTestDto>>
{
    public override void Configure()
    {
        Get(RouteConstants.TestsGetActive);
        Roles(UserRoleExtensions.GetAuthorizedRoles());

    }
    
    public override async Task<ApiResponseResult<ActiveTestDto>> ExecuteAsync(GetActiveTestRequest req, CancellationToken ct)
    {
        var userId = HttpContext.User.FindFirst("userId")?.Value;
        var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        
        if(userId is null || userEmail is null)
            return new ApiResponseResult<ActiveTestDto>(null, true, "Invalid JWT.");
        
        var result = await new GetActiveTestQuery()
        {
            Id = req.Id,
            UserId = userId,
            UserEmail = userEmail
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<ActiveTestDto>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}