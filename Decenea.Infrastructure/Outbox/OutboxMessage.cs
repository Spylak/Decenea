using Decenea.Domain.Common;

namespace Decenea.Infrastructure.Outbox;

public sealed class OutboxMessage : Entity
{
    public string CreatedBy { get; init; }

    public DateTime OccurredOnUtc { get; init; }

    public string Type { get; init; }

    public string DomainEvent { get; init; }

    public DateTime? ProcessedOnUtc { get; set; }

    public string? Error { get; set; }
}
