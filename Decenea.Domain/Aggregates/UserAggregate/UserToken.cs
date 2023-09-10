using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.UserAggregate;

public class UserToken : Entity
{
    public string UserId { get; set; }
    public string LoginProvider { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public DateTime? ExpiryTime { get; set; }
}