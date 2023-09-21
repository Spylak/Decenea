using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.LocationAggregate;

public class City : AuditableEntity
{
    public string Name { get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public double? Lat { get; set; }
    public double? Long { get; set; }
    public int CountryId { get; set; }
    public string? RegionId { get; set; }
    public string? RegionalUnitId { get; set; }
    public string? MunicipalityId { get; set; }
    public string? MunicipalUnitId { get; set; }
    public string? CommunityId { get; set; }
}