namespace Decenea.Domain.Common;

public abstract class Entity: Entity<string>
{
    public Entity(string? id = null)
    {
        Id = id ?? Ulid.NewUlid().ToString();
    }
}

public abstract class Entity<T> where T : notnull
{
    public T Id { get; init; } = default!;

    public override bool Equals(object? other)
    {
        if (other is null || other.GetType() != GetType())
        {
            return false;
        }

        return ((Entity<T>)other).Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    protected Entity(T id) => Id = id;

    protected Entity() { }
}