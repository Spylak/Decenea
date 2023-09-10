namespace Decenea.Domain.Common;

public class SourceEventStream
{
    public SourceEventStream(DateTime? dateTime = null)
    {
        Id ??= Ulid.NewUlid().ToString()!;
        TimestampUtc = dateTime ?? DateTime.UtcNow;
    }
    public string Id { get; set; }
    public required int AggregateVersion { get; set; }
    public required int SnapshotVersion { get; set; }
    //When events are being archived it has to be reseted
    public string Snapshot { get; set; }
    
    public DateTime TimestampUtc { get; init; }
    public DateTime LastEventTimestampUtc { get; set; }
}