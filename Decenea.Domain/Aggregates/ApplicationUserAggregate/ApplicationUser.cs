using Decenea.Domain.Aggregates.AdvertisementAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.ApplicationUserAggregate;

public class ApplicationUser : AggregateRoot
{
    public string CityId { get; set; }
    public string? UserName { get; set; }
    public virtual string? NormalizedUserName { get; set; }
    public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? PasswordHash { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsVerified { get; set; }
    public string? Ethnicity { get; set; }
    public required string ResidenceOf { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public string FullName => $"{FirstName} {MiddleName} {LastName}";
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public DateTime? DateVerified { get; set; }
    public ICollection<MicroAd> MicroAds { get; set; }
    public ICollection<ApplicationUserClaim> UserClaims { get; set; }
    public ICollection<ApplicationUserToken> UserTokens { get; set; }
}   