using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Entities.ApplicationUserEntities;

public class ApplicationUserLogin : IdentityUserLogin<long>
{
    public ApplicationUser User { get; set; }
}