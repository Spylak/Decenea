namespace Decenea.Common.Requests.Test;

public class GetManyTestsRequest
{
    public int? Skip { get; set; } = 0;
    public int? Take { get; set; } = 20; 
    public int CountryId { get; set; }
    public string? RegionId { get; set; }
    public string? RegionalUnitId { get; set; }
    public string? MunicipalityId { get; set; }
    public string? MunicipalUnitId { get; set; }
    public string? CommunityId { get; set; }
}