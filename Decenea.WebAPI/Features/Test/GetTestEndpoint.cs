using Decenea.Application.Tests.Queries.GetManyTests;
using Decenea.Application.Tests.Queries.GetTest;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Requests.Test;


namespace Decenea.WebAPI.Features.Test;

public class GetTestEndpoint : Endpoint<GetTestRequest, ApiResponse<TestDto>>
{
    public override void Configure()
    {
        Get("/Test/Get");
        AllowAnonymous();
    }
    
    public override async Task<ApiResponse<TestDto>> ExecuteAsync(GetTestRequest req, CancellationToken ct)
    {
        var result = await new GetTestQuery()
        {
            Id = req.Id
        }.ExecuteAsync(ct);
        
        return new ApiResponse<TestDto>(result.Value, result.IsSuccess, result.Messages);
    }
}