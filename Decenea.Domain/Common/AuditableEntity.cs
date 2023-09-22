namespace Decenea.Domain.Common;

public abstract class AuditableEntity : Entity
{
    public string Version { get; set; }
    public string CreatedBy { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime LastModifiedByTimestampUtc { get; set; }
    public DateTime CreatedByTimestampUtc { get; set; }
}