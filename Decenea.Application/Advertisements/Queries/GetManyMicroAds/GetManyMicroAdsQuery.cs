using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Advertisement;
using Mediator;

namespace Decenea.Application.Advertisements.Queries.GetManyMicroAds;

public class GetManyMicroAdsQuery : IQuery<Result<IEnumerable<MicroAdDto>,Exception>>
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 20;    
    public string? CityId { get; set; }
    public int CountryId { get; set; }
    public string? RegionId { get; set; }
    public string? RegionalUnitId { get; set; }
    public string? MunicipalityId { get; set; }
    public string? MunicipalUnitId { get; set; }
    public string? CommunityId { get; set; }
}