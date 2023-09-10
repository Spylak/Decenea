
namespace Decenea.Domain.Aggregates.UserAggregate;

public class Role
{
    public const int SuperAdmin = 1;
    public const int Admin = 2;
    public const int Member = 3;
    public const int Guest = 4;

    public static string[] AllowAny()
    {
        return typeof(Role)
            .GetProperties()
            .Select(i => i.Name)
            .ToArray();
    }

    public static string RoleName(int roleId)
    {
        return roleId switch
        {
            1 => nameof(SuperAdmin),
            2 => nameof(Admin),
            3 => nameof(Member),
            4 => nameof(Guest),
            _ => throw new ArgumentException(message: "Invalid rolId", roleId.ToString()),
        };
    }

    public static string[] AllowVerified(Role role)
    {
        return typeof(Role)
            .GetProperties()
            .Select(i => i.Name)
            .Where(i => i != nameof(Guest))
            .ToArray();
    }
}