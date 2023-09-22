namespace Decenea.Domain.Common;

public abstract record DomainEvent(string Id) : IDomainEvent
{
    protected DomainEvent() : this(Ulid.NewUlid().ToString())
    {
    }
}