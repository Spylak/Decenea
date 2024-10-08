using Decenea.Common.Common;

namespace Decenea.Domain.Common;

public abstract class Entity : Versioned
{
    public string Id { get; init; } =  Ulid.NewUlid().ToString();
    public override bool Equals(object? other)
    {
        if (other is null || other.GetType() != GetType())
        {
            return false;
        }

        return ((Entity)other).Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}