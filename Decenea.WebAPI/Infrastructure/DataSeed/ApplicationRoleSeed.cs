using Decenea.WebAPI.Domain.Constants;
using Decenea.Domain.Entities.ApplicationUserEntities;
using Microsoft.EntityFrameworkCore;

namespace Decenea.WebAPI.Infrastructure.DataSeed;

public static class ApplicationRoleSeed
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<ApplicationRole>().HasData(
            new ApplicationRole()
        {
            Id = 1,
            Name = ApplicationRoles.SuperAdmin,
            NormalizedName = ApplicationRoles.SuperAdmin.ToUpper()
        }, new ApplicationRole()
        {
            Id = 2,
            Name = ApplicationRoles.Admin,
            NormalizedName = ApplicationRoles.Admin.ToUpper()
        }, new ApplicationRole()
        {
            Id = 3,
            Name = ApplicationRoles.Member,
            NormalizedName = ApplicationRoles.Member.ToUpper()
        }, new ApplicationRole()
        {
            Id = 4,
            Name = ApplicationRoles.Guest,
            NormalizedName = ApplicationRoles.Guest.ToUpper()
        });
    }
}