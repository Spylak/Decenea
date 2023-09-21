using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Location;
using Mediator;
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

            return Result<List<CityDto>, Exception>.Anticipated(new List<CityDto>());
        }
        catch (Exception ex)
        {
            Log.Error("Error on GetManyCities: {ex}",ex);
            return Result<List<CityDto>, Exception>
                .Excepted(ex,$"Didn't manage to get list of cities.");
        }
    }
}