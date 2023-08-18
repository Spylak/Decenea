using Decenea.Domain.Entities.Common;

namespace Decenea.Domain.Entities.Location;

public class City : AuditableEntity
{
    public string Name { get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public double? Lat { get; set; }
    public double? Long { get; set; }
    public string CountryId { get; set; }
    public Country Country { get; set; }
    public string? CommunityId { get; set; }
    public Community? Community { get; set; }
}