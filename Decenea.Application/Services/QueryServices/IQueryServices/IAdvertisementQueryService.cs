using Decenea.Domain.Common;
using Decenea.Domain.DataTransferObjects.Advertisement;

namespace Decenea.Application.Services.QueryServices.IQueryServices;

public interface IAdvertisementQueryService
{
    Task<Result<List<MicroAdDto>, Exception>> GetManyMicroAds(GetManyMicroAdsRequestDto requestDto);
}