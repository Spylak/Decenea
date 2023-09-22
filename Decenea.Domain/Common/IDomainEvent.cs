using Mediator;

namespace Decenea.Domain.Common;

public interface IDomainEvent : INotification
{
    public string Id { get; init; }
}