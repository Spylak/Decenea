using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Entities.ApplicationUserEntities;

public class ApplicationRoleClaim : IdentityRoleClaim<long>
{
    public new long Id { get; set; }
    public ApplicationRole Role { get; set; }
}