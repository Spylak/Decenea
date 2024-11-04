using System.Security.Claims;
using Decenea.Application.Features.Test.Queries.GetTest;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Test;


namespace Decenea.WebAPI.Endpoints.Test;

public class GetTestEndpoint : Endpoint<GetTestRequest, ApiResponseResult<TestDto>>
{
    public override void Configure()
    {
        Get(RouteConstants.TestsGet);
        Roles(UserRoleExtensions.GetAuthorizedRoles());

    }
    
    public override async Task<ApiResponseResult<TestDto>> ExecuteAsync(GetTestRequest req, CancellationToken ct)
    {
        var userId = HttpContext.User.FindFirst("userId")?.Value;
        var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        
        if(userId is null || userEmail is null)
            return new ApiResponseResult<TestDto>(null, true, "Invalid JWT.");
        
        var result = await new GetTestQuery()
        {
            Id = req.Id,
            UserId = userId,
            UserEmail = userEmail,
            IncludeQuestions = req.IncludeQuestions
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<TestDto>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}