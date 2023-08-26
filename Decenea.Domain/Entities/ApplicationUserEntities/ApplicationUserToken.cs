using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Entities.ApplicationUserEntities;

public class ApplicationUserToken : IdentityUserToken<long>
{
    public ApplicationUser User { get; set; }
}