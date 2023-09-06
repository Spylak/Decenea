namespace Decenea.Domain.Common;

public abstract record DomainEvent(Ulid Id) : IDomainEvent
{
    protected DomainEvent() : this(Ulid.NewUlid())
    {
    }
}