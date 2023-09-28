using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.LocationAggregate;

public class Region : AuditableEntity
{
    private readonly List<RegionalUnit> _regionalUnits = new();
    public IReadOnlyList<RegionalUnit> RegionalUnits => _regionalUnits;
    public required string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public required string CountryId { get; set; }
}