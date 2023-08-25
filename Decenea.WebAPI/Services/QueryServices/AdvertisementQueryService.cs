using Decenea.WebAPI.Services.QueryServices.IQueryServices;
using Decenea.WebAPI.Domain.Common;
using Decenea.Domain.DataTransferObjects.Advertisement;
using Decenea.Domain.Entities.AdvertisementEntities;
using Decenea.WebAPI.Domain.Extensions;
using Decenea.WebAPI.Domain.Mappers;
using Decenea.WebAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Decenea.WebAPI.Services.QueryServices;

public class AdvertisementQueryService : IAdvertisementQueryService
{
    private readonly DeceneaDbContext _dbContext;

    public AdvertisementQueryService(DeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<List<MicroAdDto>, Exception>> GetManyMicroAds(GetManyMicroAdsRequestDto requestDto)
    {
        try
        {
            var microAds = await _dbContext.Set<MicroAd>()
                .Include(i => i.ApplicationUser)
                .WhereIf(requestDto.CountryId is not null, i => i.City.Country.Id == requestDto.CountryId)
                .WhereIf(requestDto.RegionId is not null, i => i.CityId == requestDto.RegionId)
                .OrderBy(i =>i.ModifiedAt)
                .Skip(requestDto.Skip)
                .Take(requestDto.Take)
                .ToListAsync();
        
            var microAdDtos = microAds
                .Select(i => i.MicroAdToMicroAdDto())
                .ToList();

            return Result<List<MicroAdDto>, Exception>.Anticipated(microAdDtos);
        }
        catch (Exception ex)
        {
            return Result<List<MicroAdDto>, Exception>
                .Excepted(ex,$"Didn't manage to get list of Micro Ads.");
        }
    }
}