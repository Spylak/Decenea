using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.LocationAggregate;

public class Country : AuditableAggregateRoot
{
    private readonly List<Region> _regions = new();
    public IReadOnlyList<Region> Regions => _regions;
    public string Name { get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string CountryCode { get; set; }
    public string Timezone { get; set; }
}