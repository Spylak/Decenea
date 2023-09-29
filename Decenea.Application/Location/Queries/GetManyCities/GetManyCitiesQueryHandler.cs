using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Location;
using Decenea.Domain.Aggregates.LocationAggregate;
using Decenea.Domain.Extensions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Location.Queries.GetManyCities;

public class GetManyCitiesQueryHandler : IQueryHandler<GetManyCitiesQuery,Result<List<CityDto>, Exception>>
{
    private readonly IDeceneaDbContext _dbContext;
    public GetManyCitiesQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async ValueTask<Result<List<CityDto>, Exception>> Handle(GetManyCitiesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var cities = await _dbContext.Set<City>()
                .Take(query.Take)
                .Skip(query.Skip)
                .WhereIf(query.CountryId is not null,i => i.CountryId == query.CountryId)
                .WhereIf(query.CommunityId is not null,i => i.CommunityId == query.CommunityId)
                .WhereIf(query.RegionId is not null,i => i.RegionId == query.RegionId)
                .Select(i => i.CityToCityDto(null))
                .ToListAsync(cancellationToken);
            
            return Result<List<CityDto>, Exception>.Anticipated(cities);
        }
        catch (Exception ex)
        {
            Log.Error("Error on GetManyCities: {ex}",ex);
            return Result<List<CityDto>, Exception>
                .Excepted(ex,$"Didn't manage to get list of cities.");
        }
    }
}