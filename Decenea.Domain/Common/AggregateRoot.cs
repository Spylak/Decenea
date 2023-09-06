namespace Decenea.Domain.Common;
public abstract class AggregateRoot<T> : Entity<T> where T : notnull
{
    protected AggregateRoot() { }

    protected readonly Queue<IDomainEvent> _domainEvents = new();

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }
}

public abstract class AggregateRoot : AggregateRoot<string>
{
}