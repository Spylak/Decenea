namespace Decenea.Domain.Common;

public interface IAuditableEntity
{
    public DateTime LastModifiedByTimestampUtc { get; set; }
    public DateTime CreatedByTimestampUtc { get; set; }
    public string CreatedBy { get; set; }
    public string LastModifiedBy { get; set; }
    public string Version { get; set; }
}