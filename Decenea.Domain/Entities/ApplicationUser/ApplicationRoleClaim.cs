using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Entities.ApplicationUser;

public class ApplicationRoleClaim : IdentityRoleClaim<long>
{
    public new long Id { get; set; }
    public ApplicationRole Role { get; set; }
}