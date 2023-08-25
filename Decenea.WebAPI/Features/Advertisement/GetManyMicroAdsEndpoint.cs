using Decenea.WebAPI.Services.QueryServices.IQueryServices;
using Decenea.WebAPI.Domain.Common;
using Decenea.Domain.DataTransferObjects.Advertisement;

namespace Decenea.WebAPI.Features.Advertisement;

public class GetManyMicroAds : Endpoint<GetManyMicroAdsRequestDto, ApiResponse<List<MicroAdDto>>>
{
    private readonly IAdvertisementQueryService _advertisementQueryService;
    public GetManyMicroAds(IAdvertisementQueryService advertisementQueryService)
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