using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.ApplicationUserAggregate;

public class ApplicationUserToken : Entity
{
    public string UserId { get; set; }
    public string LoginProvider { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}