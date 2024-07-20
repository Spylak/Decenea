namespace Decenea.Domain.Common;

public interface IAuditable
{
    public string CreatedBy { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime LastModifiedByTimestampUtc { get; set; }
    public DateTime CreatedByTimestampUtc { get; set; }
}