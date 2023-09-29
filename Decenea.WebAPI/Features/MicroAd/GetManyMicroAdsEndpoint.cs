using Decenea.Application.MicroAds.Queries.GetManyMicroAds;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Advertisement;
using Decenea.Common.Requests.MicroAds;
using Mediator;

namespace Decenea.WebAPI.Features.MicroAd;

public class GetManyMicroAdsEndpoint : Endpoint<GetManyMicroAdsRequest, ApiResponse<IEnumerable<MicroAdDto>>>
{
    private readonly IMediator _mediator;
    public GetManyMicroAdsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Get("/MicroAd/GetMany");
        AllowAnonymous();
    }
    
    public override async Task<ApiResponse<IEnumerable<MicroAdDto>>> ExecuteAsync(GetManyMicroAdsRequest req, CancellationToken ct)
    {
        var query = new GetManyMicroAdsQuery
        {
            Skip = req.Skip,
            Take = req.Take,
            CityId = req.CityId,
            CommunityId = req.CommunityId,
            MunicipalUnitId = req.MunicipalUnitId,
            MunicipalityId = req.MunicipalityId,
            RegionalUnitId = req.RegionalUnitId,
            RegionId = req.RegionId,
            CountryId = req.CountryId,
        };
        var result = await _mediator.Send(query);
        return new ApiResponse<IEnumerable<MicroAdDto>>(result.Value, result.IsSuccess, result.Messages);
    }
}