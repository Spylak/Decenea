using Decenea.Domain.Entities.AdvertisementEntities;
using Decenea.Domain.Entities.Common;
using Decenea.Domain.Entities.LocationEntities;
using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Entities.ApplicationUserEntities;

public class ApplicationUser : IdentityUser<long> , IAuditableEntity
{
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
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTime DateVerified { get; set; }
    public string CityId { get; set; }
    public City City { get; set; }
    public ICollection<MicroAd> MicroAds { get; set; }
    public ICollection<ApplicationUserRole> UserRoles { get; set; }
    public ICollection<ApplicationUserLogin> UserLogins { get; set; }
    public ICollection<ApplicationUserClaim> UserClaims { get; set; }
    public ICollection<ApplicationUserToken> UserTokens { get; set; }
}   