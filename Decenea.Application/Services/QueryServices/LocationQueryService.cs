using Decenea.Application.Extensions;
using Decenea.Application.Mappers;
using Decenea.Application.Services.QueryServices.IQueryServices;
using Decenea.Domain.Common;
using Decenea.Domain.DataTransferObjects.Location;
using Decenea.Domain.Entities.LocationEntities;
using Decenea.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Services.QueryServices;

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