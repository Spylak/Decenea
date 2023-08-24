using Decenea.Domain.Entities.AdvertisementEntities;
using Decenea.Domain.Entities.ApplicationUserEntities;
using Decenea.Domain.Entities.Common;

namespace Decenea.Domain.Entities.LocationEntities;

public class City : AuditableEntity
{
    public string Name { get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public double? Lat { get; set; }
    public double? Long { get; set; }
    public string CountryId { get; set; }
    public Country Country { get; set; }
    public string? RegionId { get; set; }
    public Region? Region { get; set; }
    public string? CommunityId { get; set; }
    public Community? Community { get; set; }
    public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    public ICollection<MicroAd> MicroAds { get; set; }
}