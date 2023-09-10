using Decenea.Domain.Common;

namespace Decenea.Infrastructure.Outbox;

public sealed class OutboxMessage
{
    public OutboxMessage(Ulid id, DateTime occurredOnUtc, string type, IDomainEvent domainEvent, string createdBy, string? error = null)
    {
        Id = id;
        OccurredOnUtc = occurredOnUtc;
        DomainEvent = domainEvent;
        Type = type;
        CreatedBy = createdBy;
        Error = error;
    }

    public Ulid Id { get; init; }
    public string CreatedBy { get; init; }

    public DateTime OccurredOnUtc { get; init; }

    public string Type { get; init; }

    public IDomainEvent DomainEvent { get; init; }

    public DateTime? ProcessedOnUtc { get; set; }

    public string? Error { get; set; }
}
