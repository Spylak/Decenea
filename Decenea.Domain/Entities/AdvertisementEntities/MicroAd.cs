using Decenea.Domain.Entities.ApplicationUserEntities;
using Decenea.Domain.Entities.Common;
using Decenea.Domain.Entities.LocationEntities;

namespace Decenea.Domain.Entities.AdvertisementEntities;

public class MicroAd : AuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public long ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public string CityId { get; set; }
    public City City { get; set; }
}