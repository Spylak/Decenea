using Decenea.WebAPI.Services.QueryServices.IQueryServices;
using Decenea.WebAPI.Domain.Common;
using Decenea.Domain.DataTransferObjects.Location;
using Decenea.Domain.Entities.LocationEntities;
using Decenea.WebAPI.Domain.Extensions;
using Decenea.WebAPI.Domain.Mappers;
using Decenea.WebAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.WebAPI.Services.QueryServices;

public class LocationQueryService : ILocationQueryService
{
    private readonly DeceneaDbContext _dbContext;

    public LocationQueryService(DeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<CityDto>, Exception>> GetManyCities(GetManyCitiesRequestDto requestDto)
    {
        try
        {
            var cities = await _dbContext.Set<City>()
                .WhereIf(requestDto.RegionId is not null, i => i.RegionId == requestDto.RegionId)
                .WhereIf(requestDto.CommunityId is not null, i => i.CommunityId == requestDto.CommunityId)
                .WhereIf(requestDto.CountryId is not null, i => i.CountryId == requestDto.CountryId)
                .WhereIf(requestDto.RegionName is not null, i => i.Region != null && i.Region.Name == requestDto.RegionName)
                .WhereIf(requestDto.CommunityName is not null, i => i.Community != null && i.Community.Name == requestDto.CommunityName)
                .WhereIf(requestDto.CountryName is not null, i => i.Country.Name == requestDto.CountryName)
                .OrderBy(i =>i.Name)
                .Skip(requestDto.Skip)
                .Take(requestDto.Take)
                .ToListAsync();
        
            var cityDtos = cities
                .Select(i => i.CityToCityDto())
                .ToList();

            return Result<List<CityDto>, Exception>.Anticipated(cityDtos);
        }
        catch (Exception ex)
        {
            Log.Error("Error on GetManyCities: {ex}",ex);
            return Result<List<CityDto>, Exception>
                .Excepted(ex,$"Didn't manage to get list of cities.");
        }
    }
}