namespace Decenea.Domain.Common;

public abstract class LinkingTableEntity : ICreatedByProperties
{
    public string CreatedBy { get; set; }
    public DateTime CreatedByTimestampUtc { get; set; }
}