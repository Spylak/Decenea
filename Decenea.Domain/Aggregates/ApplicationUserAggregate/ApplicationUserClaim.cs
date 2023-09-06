using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Aggregates.ApplicationUserAggregate;

public class ApplicationUserClaim
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
}