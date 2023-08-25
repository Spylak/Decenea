using System.Text;
using Decenea.Domain.DataTransferObjects.Location;
using Decenea.Domain.Entities.LocationEntities;

namespace Decenea.WebAPI.Domain.Mappers;

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

    public static string GetLocation(City city)
    {
        StringBuilder strBldr = new();
        strBldr.Append(city.Name);
        
        var regionalUnit = city.Community?.MunicipalUnit?.Municipality?.RegionalUnit.Region.Name;
        if (regionalUnit is not null)
        {
            strBldr.Append(',' + regionalUnit);
        }
        
        var region = city.Community?.MunicipalUnit?.Municipality?.RegionalUnit.Name;
        if (region is not null)
        {
            strBldr.Append(',' + region);
        }
        
        strBldr.Append(',' + city.Country.Name);
        
        return strBldr.ToString();
    }
}