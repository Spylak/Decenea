using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.UserAggregate;

public class UserSourceEvent : SourceEvent
{
    public UserSourceEvent(int sequenceNo, string eventData, string createdBy, DateTime? dateTime = null) : base(sequenceNo, eventData, createdBy, dateTime)
    {
    }
    public UserSourceEventStream UserSourceEventStream { get; set; }
}