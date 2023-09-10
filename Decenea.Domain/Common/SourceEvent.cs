using System.Text.Json;

namespace Decenea.Domain.Common;

public class SourceEvent
{
    public SourceEvent(int sequenceNo, string eventData, string createdBy, DateTime? dateTime = null)
    {
        TimestampUtc = dateTime ?? DateTime.UtcNow;
        Id ??= Ulid.NewUlid().ToString()!;
        CreatedBy = createdBy;
        EventData = eventData;
        SequenceNo = sequenceNo;
    }
    public string Id { get; set; }
    public string StreamId { get; set; }
    public int SequenceNo { get; set; }
    public DateTime TimestampUtc { get; init; }
    public string CreatedBy { get; set; }
    public string EventData { get; set; }
    //If any events are being archived then the snapshot on the event stream has to reset
    public bool IsArchived { get; set; }
}