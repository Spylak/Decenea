using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.UserAggregate;

public class UserClaim : AuditableEntity
{
    public string UserId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
}