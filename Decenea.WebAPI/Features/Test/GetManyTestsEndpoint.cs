using Decenea.Application.Tests.Queries.GetManyTests;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Requests.Test;


namespace Decenea.WebAPI.Features.Test;

public class GetManyTestsEndpoint : Endpoint<GetManyTestsRequest, ApiResponse<IEnumerable<TestDto>>>
{
    private readonly GetManyTestsQueryHandler _getManyTestsQueryHandler;
    public GetManyTestsEndpoint(GetManyTestsQueryHandler getManyTestsQueryHandler)
    {
        _getManyTestsQueryHandler = getManyTestsQueryHandler;
    }
    
    public override void Configure()
    {
        Get("/Test/GetMany");
        AllowAnonymous();
    }
    
    public override async Task<ApiResponse<IEnumerable<TestDto>>> ExecuteAsync(GetManyTestsRequest req, CancellationToken ct)
    {
        var query = new GetManyTestsQuery
        {
            Skip = req.Skip,
            Take = req.Take,
            CommunityId = req.CommunityId,
            MunicipalUnitId = req.MunicipalUnitId,
            MunicipalityId = req.MunicipalityId,
            RegionalUnitId = req.RegionalUnitId,
            RegionId = req.RegionId,
            CountryId = req.CountryId,
        };
        var result = await _getManyTestsQueryHandler.Handle(query, ct);
        return new ApiResponse<IEnumerable<TestDto>>(result.Value, result.IsSuccess, result.Messages);
    }
}