using Decenea.Domain.Entities.Common;

namespace Decenea.Domain.Entities.LocationEntities;

public class RegionalUnit : AuditableEntity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string RegionId { get; set; }
    public Region Region { get; set; }
    public ICollection<Municipality> Municipalities { get; set; }
}