using Decenea.Common.Common;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.MicroAdAggregate;

public class MicroAd : AuditableAggregateRoot
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public string UserId { get; set; }
    public string CityId { get; set; }

    public static Result<MicroAd, Exception> Create(string title, string descripton,
        string cityId, string userId,
        string contactEmail,
        string contactPhone)
    {
        var newMicroAd = new MicroAd()
        {
            Title = title,
            Description = descripton,
            CityId = cityId,
            UserId = userId,
            ContactEmail = contactEmail,
            ContactPhone = contactPhone
        };
        
        return Result<MicroAd, Exception>.Anticipated(newMicroAd);
    }
    
    public static Result<MicroAd, Exception> Update(MicroAd microAd, string title, string descripton,
        string cityId,
        string contactEmail,
        string contactPhone)
    {
        microAd.Title = title;
        microAd.Description = descripton;
        microAd.CityId = cityId;
        microAd.ContactEmail = contactEmail;
        microAd.ContactPhone = contactPhone;
        
        return Result<MicroAd, Exception>.Anticipated(microAd);
    }
}