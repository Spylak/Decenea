using Decenea.Domain.Entities.Common;

namespace Decenea.Domain.Entities.Location;

public class MunicipalUnit : AuditableEntity
{
    public string Name {get; set; }
    public string? AsciiName { get; set; }
    public string? AlternativeName { get; set; }
    public string MunicipalityId { get; set; }
    public Municipality Municipality { get; set; }
    public List<Community> Communities { get; set; }
}