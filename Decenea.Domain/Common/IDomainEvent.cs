using FastEndpoints;

namespace Decenea.Domain.Common;

public interface IDomainEvent : IEvent
{
    public string Id { get; init; }
}