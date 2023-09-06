using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.AdvertisementAggregate;

public class MicroAd : AggregateRoot
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public long ApplicationUserId { get; set; }
    public string CityId { get; set; }
}