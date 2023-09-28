using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.LocationAggregate;

public class MunicipalUnit : AuditableEntity
{
    private readonly List<Community> _communities = new();
    public IReadOnlyList<Community> Communities => _communities;
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string MunicipalityId { get; set; }
}