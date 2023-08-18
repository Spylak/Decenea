using Decenea.Domain.Entities.Common;

namespace Decenea.Domain.Entities.Location;

public class Region : AuditableEntity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string CountryId { get; set; }
    public Country Country { get; set; }
    public ICollection<RegionalUnit> RegionalUnits { get; set; }
}