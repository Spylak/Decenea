using Decenea.Domain.Helpers;

namespace Decenea.Domain.Common;

public abstract class LinkingTableEntity : IAuditableEntity
{
    public string CreatedBy { get; set; }
    public DateTime CreatedByTimestampUtc { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime LastModifiedByTimestampUtc { get; set; }
    public string Version { get; set; } = RandomStringGenerator.RandomString(8);
}