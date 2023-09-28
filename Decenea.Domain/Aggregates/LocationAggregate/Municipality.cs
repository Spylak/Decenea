using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.LocationAggregate;

public class Municipality : AuditableEntity
{
    private readonly List<MunicipalUnit> _municipalUnits = new();
    public IReadOnlyList<MunicipalUnit> MunicipalUnits => _municipalUnits;
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string RegionalUnitId { get; set; }
}