using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.LocationAggregate;

public class RegionalUnit : AuditableEntity
{
    private readonly List<Municipality> _municipalities = new();
    public IReadOnlyList<Municipality> Municipalities => _municipalities;
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string RegionId { get; set; }
}