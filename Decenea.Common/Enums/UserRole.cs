
using Decenea.Common.Extensions;

namespace Decenea.Common.Enums;

public enum UserRole
{
    SuperAdmin = 1,
    Admin = 2,
    Member = 3,
    Guest = 4
}

public static class UserRoleExtensions
{
    public static string[] GetAllRoles()
    {
        return EnumExtensions.GetNames<UserRole>();
    }
    
    public static string[] GetAuthorizesRoles()
    {
        return EnumExtensions.GetNames<UserRole>()
            .Where(i => i != nameof(UserRole.Guest))
            .ToArray();
    }
}