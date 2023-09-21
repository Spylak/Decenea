using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.LocationAggregate;

public class Municipality : AuditableEntity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string RegionalUnitId { get; set; }
}