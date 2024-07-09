
namespace Decenea.Domain.Aggregates.GroupAggregate;

public class GroupRole
{
    public const int Owner = 1;
    public const int Member = 2;

    public static string[] AllowAny()
    {
        return typeof(GroupRole)
            .GetProperties()
            .Select(i => i.Name)
            .ToArray();
    }

    public static string RoleName(int roleId)
    {
        return roleId switch
        {
            1 => nameof(Owner),
            2 => nameof(Member),
            _ => throw new ArgumentException(message: "Invalid rolId", roleId.ToString()),
        };
    }
}