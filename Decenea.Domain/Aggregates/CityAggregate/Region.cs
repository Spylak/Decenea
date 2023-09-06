using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.CityAggregate;

public class Region : Entity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string? CountryId { get; set; }
}