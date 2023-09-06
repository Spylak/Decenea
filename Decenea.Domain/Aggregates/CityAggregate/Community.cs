using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.CityAggregate;

public class Community : Entity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string? MunicipalUnitId { get; set; }
}