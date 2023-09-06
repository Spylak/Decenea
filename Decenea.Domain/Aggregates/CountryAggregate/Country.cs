using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.CountryAggregate;

public class Country : AggregateRoot<int>
{
    public string Name { get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string CountryCode { get; set; }
    public string Timezone { get; set; }
}