using Decenea.Application.Abstractions.Persistance.IRepositories;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Advertisement;
using Mediator;

namespace Decenea.Application.MicroAds.Queries.GetManyMicroAds;

public class GetManyMicroAdsQueryHandler : IQueryHandler<GetManyMicroAdsQuery, Result<IEnumerable<MicroAdDto>, Exception>>
{
    private readonly IMicroAdRepository _microAdRepository;

    public GetManyMicroAdsQueryHandler(IMicroAdRepository microAdRepository)
    {
        _microAdRepository = microAdRepository;
    }

    public async ValueTask<Result<IEnumerable<MicroAdDto>, Exception>> Handle(GetManyMicroAdsQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var microAdDtos = await _microAdRepository
                .GetManyMicroAdDtos(query.CountryId,
                    query.CityId,
                    query.CommunityId,
                    query.MunicipalUnitId,
                    query.MunicipalityId,
                    query.RegionalUnitId,
                    query.RegionId);

            return Result<IEnumerable<MicroAdDto>, Exception>.Anticipated(microAdDtos);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<MicroAdDto>, Exception>
                .Excepted(ex, "Didn't manage to get list of Micro Ads.");
        }
    }
}