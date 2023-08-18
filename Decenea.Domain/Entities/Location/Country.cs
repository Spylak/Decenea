using Decenea.Domain.Entities.Common;

namespace Decenea.Domain.Entities.Location;

public class Country : AuditableEntity
{
    public string Name { get; set; }
    public string CountryCode { get; set; }
    public string Timezone { get; set; }
    public ICollection<Region> Regions { get; set; }
    public ICollection<City> Cities { get; set; }
}