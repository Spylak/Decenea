using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Entities.ApplicationUser;

public class ApplicationUserLogin : IdentityUserLogin<long>
{
    public ApplicationUser User { get; set; }
}