using Decenea.Domain.Common;
using FastEndpoints;

namespace Decenea.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> : IEventHandler<TEvent>
    where TEvent : IDomainEvent
{
}
