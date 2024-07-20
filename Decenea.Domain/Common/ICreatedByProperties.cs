namespace Decenea.Domain.Common;

public interface ICreatedByProperties
{
    public string CreatedBy { get; set; }
    public DateTime CreatedByTimestampUtc { get; set; }
}