using Decenea.Domain.Entities.Common;

namespace Decenea.Domain.Entities.Location;

public class Community : AuditableEntity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string MunicipalUnitId { get; set; }
    public MunicipalUnit MunicipalUnit { get; set; }
    public List<City> Cities { get; set; }
}