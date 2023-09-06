using System.Text;
using Decenea.Domain.Aggregates.CityAggregate;
using Decenea.Domain.Aggregates.LocationAggregate;
using Decenea.Shared.DataTransferObjects.Location;

namespace Decenea.Domain.Mappers;

public static class LocationMapper
{
    public static CityDto CityToCityDto(this City city, CityDto? cityDto = null)
    {
        cityDto ??= new CityDto();
        cityDto.Id ??= city.Id;
        cityDto.Name ??= city.Name;
        cityDto.CountryId ??= city.CountryId;
        cityDto.CommunityId ??= city.CommunityId;
        cityDto.RegionId ??= city.RegionId;
        return cityDto;
    }
}