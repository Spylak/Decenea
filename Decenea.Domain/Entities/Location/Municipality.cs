using Decenea.Domain.Entities.Common;

namespace Decenea.Domain.Entities.Location;

public class Municipality : AuditableEntity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string RegionalUnitId { get; set; }
    public RegionalUnit RegionalUnit { get; set; }
    public ICollection<MunicipalUnit> MunicipalUnits { get; set; }
}