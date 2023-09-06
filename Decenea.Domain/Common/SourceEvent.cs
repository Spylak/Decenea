using System.Text.Json;

namespace Decenea.Domain.Common;

public record SourceEvent
{
    public SourceEvent(int sequenceNo, string eventData, string eventType, string createdBy, DateTime? dateTime = null)
    {
        TimestampUtc = dateTime ?? DateTime.UtcNow;
        EventType = eventType;
        Id = Ulid.NewUlid().ToString()!;
        CreatedBy = createdBy;
        EventData = eventData;
        SequenceNo = sequenceNo;
    }
    public string Id { get; init; }
    public int SequenceNo { get; init; }
    public string EventType { get; init; }
    public DateTime TimestampUtc { get; init; }
    public string CreatedBy { get; init; }
    public string EventData { get; init; }
}