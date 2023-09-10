using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.LocationAggregate;

public class RegionalUnit : Entity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string? RegionId { get; set; }
}