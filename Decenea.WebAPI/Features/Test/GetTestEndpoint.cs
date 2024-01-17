using Decenea.Application.Tests.Queries.GetManyTests;
using Decenea.Application.Tests.Queries.GetTest;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Requests.Test;


namespace Decenea.WebAPI.Features.Test;

public class GetTestEndpoint : Endpoint<GetTestRequest, ApiResponse<TestDto>>
{
    private readonly GetTestQueryHandler _getTestQueryHandler;
    public GetTestEndpoint(GetTestQueryHandler getTestQueryHandler)
    {
        _getTestQueryHandler = getTestQueryHandler;
    }
    
    public override void Configure()
    {
        Get("/Test/Get");
        AllowAnonymous();
    }
    
    public override async Task<ApiResponse<TestDto>> ExecuteAsync(GetTestRequest req, CancellationToken ct)
    {
        var test = new GetTestQuery()
        {
            Id = req.Id
        };
        
        var result = await _getTestQueryHandler.Handle(test, ct);
        return new ApiResponse<TestDto>(result.Value, result.IsSuccess, result.Messages);
    }
}