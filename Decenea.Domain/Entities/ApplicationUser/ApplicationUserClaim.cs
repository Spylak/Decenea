using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Entities.ApplicationUser;

public class ApplicationUserClaim : IdentityUserClaim<long>
{
    public new long Id { get; set; }
    public ApplicationUser User { get; set; }
}