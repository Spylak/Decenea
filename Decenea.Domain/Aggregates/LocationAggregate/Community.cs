using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.LocationAggregate;

public class Community : AuditableEntity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string MunicipalUnitId { get; set; }
}