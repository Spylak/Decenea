using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.CityAggregate;

public class City : AggregateRoot
{
    public string Name { get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public double? Lat { get; set; }
    public double? Long { get; set; }
    public int CountryId { get; set; }
    public string? RegionId { get; set; }
    public string? CommunityId { get; set; }
}