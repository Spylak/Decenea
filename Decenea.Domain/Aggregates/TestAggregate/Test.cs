using Decenea.Common.Common;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.TestAggregate;

public class Test : AuditableAggregateRoot
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public string UserId { get; set; }
    public string CityId { get; set; }

    public static Result<Test, Exception> Create(string title, string descripton,
        string cityId, string userId,
        string contactEmail,
        string contactPhone)
    {
        var test = new Test()
        {
            Title = title,
            Description = descripton,
            CityId = cityId,
            UserId = userId,
            ContactEmail = contactEmail,
            ContactPhone = contactPhone
        };
        
        return Result<Test, Exception>.Anticipated(test);
    }
    
    public static Result<Test, Exception> Update(Test test, string title, string descripton,
        string cityId,
        string contactEmail,
        string contactPhone)
    {
        test.Title = title;
        test.Description = descripton;
        test.CityId = cityId;
        test.ContactEmail = contactEmail;
        test.ContactPhone = contactPhone;
        
        return Result<Test, Exception>.Anticipated(test);
    }
}