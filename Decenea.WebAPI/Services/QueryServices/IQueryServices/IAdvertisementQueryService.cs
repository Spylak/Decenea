using Decenea.WebAPI.Domain.Common;
using Decenea.Domain.DataTransferObjects.Advertisement;

namespace Decenea.WebAPI.Services.QueryServices.IQueryServices;

public interface IAdvertisementQueryService
{
    Task<Result<List<MicroAdDto>, Exception>> GetManyMicroAds(GetManyMicroAdsRequestDto requestDto);
}