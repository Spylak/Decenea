namespace Decenea.Domain.Common;

public abstract class AuditableEntity : Entity, IAuditableEntity
{
    public DateTime LastModifiedByTimestampUtc { get; set; }
    public DateTime CreatedByTimestampUtc { get; set; }
    public string CreatedBy { get; set; }
    public string LastModifiedBy { get; set; }
}