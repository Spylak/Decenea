using Mediator;

namespace Decenea.Domain.Common;

public interface IDomainEvent : INotification
{
    public Ulid Id { get; init; }
}