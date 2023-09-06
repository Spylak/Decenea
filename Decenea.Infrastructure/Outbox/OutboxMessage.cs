namespace Decenea.Infrastructure.Outbox;

public sealed class OutboxMessage
{
    public OutboxMessage(Ulid id, DateTime occurredOnUtc, string type, string content, string userId)
    {
        Id = id;
        OccurredOnUtc = occurredOnUtc;
        Content = content;
        Type = type;
        UserId = userId;
    }

    public Ulid Id { get; init; }
    public string UserId { get; init; }

    public DateTime OccurredOnUtc { get; init; }

    public string Type { get; init; }

    public string Content { get; init; }

    public DateTime? ProcessedOnUtc { get; set; }

    public string? Error { get; set; }
}
