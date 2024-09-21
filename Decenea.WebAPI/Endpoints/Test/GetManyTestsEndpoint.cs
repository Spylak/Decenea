using Decenea.Application.Features.Test.Queries.GetManyTests;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.Test;


namespace Decenea.WebAPI.Endpoints.Test;

public class GetManyTestsEndpoint : Endpoint<GetManyTestsRequest, ApiResponseResult<IEnumerable<TestDto>>>
{
    public override void Configure()
    {
        Get("/tests/get-many");
        AllowAnonymous();
    }
    
    public override async Task<ApiResponseResult<IEnumerable<TestDto>>> ExecuteAsync(GetManyTestsRequest req, CancellationToken ct)
    {
        var result = await new GetManyTestsQuery
        {
            Skip = req.Skip,
            Take = req.Take
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<IEnumerable<TestDto>>(result.Value, result.IsError, result.ErrorsOrEmptyList.ToErrorDictionary());
    }
}