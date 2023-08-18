using Decenea.Domain.Entities.Common;

namespace Decenea.Domain.Entities.Location;

public class Prefecture : AuditableEntity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string CountryId { get; set; }
    public Country Country { get; set; }
    public string RegionId { get; set; }
    public Region Region { get; set; }
    public ICollection<Municipality> Municipalities { get; set; }
    public ICollection<MunicipalUnit> MunicipalUnits { get; set; }
    public ICollection<City> Cities { get; set; }
}