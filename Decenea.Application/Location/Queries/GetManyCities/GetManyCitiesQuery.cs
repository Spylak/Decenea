using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Location;
using Mediator;

namespace Decenea.Application.Location.Queries.GetManyCities;

public class GetManyCitiesQuery : IQuery<Result<List<CityDto>, Exception>>
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 20;
    public int? CountryId { get; set; }
    public string? CountryName { get; set; }
    public string? CommunityId { get; set; }
    public string? CommunityName { get; set; }
    public string? RegionId { get; set; }
    public string? RegionName { get; set; }
}