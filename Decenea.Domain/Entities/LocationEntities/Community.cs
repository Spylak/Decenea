using Decenea.Domain.Entities.Common;

namespace Decenea.Domain.Entities.LocationEntities;

public class Community : AuditableEntity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string MunicipalUnitId { get; set; }
    public MunicipalUnit MunicipalUnit { get; set; }
    public ICollection<City> Cities { get; set; }
}