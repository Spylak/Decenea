using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.UserAggregate;

public class UserSourceEventStream : SourceEventStream
{
    public ICollection<UserSourceEvent> UserSourceEvents { get; set; } = new List<UserSourceEvent>();
}