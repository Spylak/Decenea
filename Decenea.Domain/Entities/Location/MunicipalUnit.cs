using Decenea.Domain.Entities.Common;

namespace Decenea.Domain.Entities.Location;

public class MunicipalUnit : AuditableEntity
{
    
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string CountryId { get; set; }
    public Country Country { get; set; }
    public string RegionId { get; set; }
    public Region Region { get; set; }
    public string PrefectureId { get; set; }
    public Prefecture Prefecture { get; set; }
    public string MunicipalityId { get; set; }
    public Municipality Municipality { get; set; }
    public List<City> Cities { get; set; }
}