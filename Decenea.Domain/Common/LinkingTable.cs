namespace Decenea.Domain.Common;

public abstract class LinkingTable : Versioned, IAuditableEntity
{
    public string CreatedBy { get; set; }
    public DateTime CreatedByTimestampUtc { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime LastModifiedByTimestampUtc { get; set; }
}