using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.AdvertisementAggregate;

public class MicroAd : AuditableAggregateRoot
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public string UserId { get; set; }
    public string CityId { get; set; }
}