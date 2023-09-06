using Decenea.Domain.Aggregates.LocationAggregate;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.CityAggregate;

public class Municipality : Entity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string? RegionalUnitId { get; set; }
}