using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Entities.ApplicationUser;

public class ApplicationUserToken : IdentityUserToken<long>
{
    public ApplicationUser User { get; set; }
}