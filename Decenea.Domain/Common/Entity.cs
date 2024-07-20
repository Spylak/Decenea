using Decenea.Domain.Helpers;

namespace Decenea.Domain.Common;

public abstract class Entity
{
    public string Id { get; set; } =  Ulid.NewUlid().ToString()!;
    public string Version { get; set; } = RandomStringGenerator.RandomString(8);
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