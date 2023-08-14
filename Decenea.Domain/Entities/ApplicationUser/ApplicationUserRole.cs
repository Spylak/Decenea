using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Entities.ApplicationUser;

public class ApplicationUserRole : IdentityUserRole<long>
{
    public ApplicationUser User { get; set; }
    public ApplicationRole Role { get; set; }
}