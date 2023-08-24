using Decenea.Application.Services.QueryServices.IQueryServices;
using Decenea.Domain.Common;
using Decenea.Domain.DataTransferObjects.Advertisement;

namespace Decenea.WebAPI.Endpoints.AdvertisementEndpoints;

public class GetManyMicroAdsEndpoint : Endpoint<GetManyMicroAdsRequestDto, ApiResponse<List<MicroAdDto>>>
{
    private readonly IAdvertisementQueryService _advertisementQueryService;
    public GetManyMicroAdsEndpoint(IAdvertisementQueryService advertisementQueryService)
    {
        _advertisementQueryService = advertisementQueryService;
    }
    
    public override void Configure()
    {
        Get("/MicroAd/GetMany");
        AllowAnonymous();
    }
    
    public override async Task<ApiResponse<List<MicroAdDto>>> ExecuteAsync(GetManyMicroAdsRequestDto req, CancellationToken ct)
    {
        var result = await _advertisementQueryService.GetManyMicroAds(req);
        return new ApiResponse<List<MicroAdDto>>(result.Value, result.IsSuccess, result.Message);
    }
}