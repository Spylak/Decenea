namespace Decenea.Domain.DataTransferObjects.Advertisement;

public class GetManyMicroAdsRequestDto
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 20;    
    public string? CityId { get; set; }

    public string? CityName { get; set; }

    public string? CountryId { get; set; }
    public string? CountryName { get; set; }
    public string? CommunityId { get; set; }
    public string? CommunityName { get; set; }
    public string? RegionId { get; set; }
    public string? RegionName { get; set; }
}