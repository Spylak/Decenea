using Decenea.Application.Tests.Queries.GetManyTests;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Requests.Test;


namespace Decenea.WebAPI.Features.Test;

public class GetManyTestsEndpoint : Endpoint<GetManyTestsRequest, ApiResponse<IEnumerable<TestDto>>>
{
    public override void Configure()
    {
        Get("/Test/GetMany");
        AllowAnonymous();
    }
    
    public override async Task<ApiResponse<IEnumerable<TestDto>>> ExecuteAsync(GetManyTestsRequest req, CancellationToken ct)
    {
        var result = await new GetManyTestsQuery
        {
            Skip = req.Skip,
            Take = req.Take
        }.ExecuteAsync(ct);
        
        return new ApiResponse<IEnumerable<TestDto>>(result.SuccessValue, result.IsSuccess, result.Messages);
    }
}