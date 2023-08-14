using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Entities.ApplicationUser;

public class ApplicationRole : IdentityRole<long>
{
    public ICollection<ApplicationUserRole> UserRoles { get; set; }
    public ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
}