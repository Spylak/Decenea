namespace Decenea.Domain.Common;

public abstract class AuditableAggregateRoot : AuditableEntity
{
    protected AuditableAggregateRoot() { }

    protected readonly Queue<IDomainEvent> _domainEvents = new ();

    public Queue<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return new Queue<IDomainEvent>(copy);
    }
}