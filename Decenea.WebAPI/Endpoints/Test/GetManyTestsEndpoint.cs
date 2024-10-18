using Decenea.Application.Features.Test.Queries.GetManyTests;
using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Enums;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Test;


namespace Decenea.WebAPI.Endpoints.Test;

public class GetManyTestsEndpoint : Endpoint<GetManyTestsRequest, ApiResponseResult<IEnumerable<TestDto>>>
{
    public override void Configure()
    {
        Get(RouteConstants.TestsGetMany);
        Roles(UserRoleExtensions.GetAuthorizedRoles());
    }
    
    public override async Task<ApiResponseResult<IEnumerable<TestDto>>> ExecuteAsync(GetManyTestsRequest req, CancellationToken ct)
    {
        var userId = HttpContext.User.FindFirst("userId")?.Value;
        
        if(userId is null)
            return new ApiResponseResult<IEnumerable<TestDto>>(null, true, "Invalid JWT.");

        var result = await new GetManyTestsQuery
        {
            UserId = userId,
            Skip = req.Skip,
            Take = req.Take,
            IncludeDetails = req.IncludeDetails
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<IEnumerable<TestDto>>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}