namespace Decenea.Domain.Common;

public class AuditableEntity : AuditableEntity<string>
{
    
}

public class AuditableEntity<T> : Entity<T> where T : notnull
{
    public string CreatedBy { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime LastModifiedByTimestampUtc { get; set; }
    public DateTime CreatedByTimestampUtc { get; set; }
}