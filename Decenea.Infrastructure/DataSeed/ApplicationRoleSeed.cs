using Decenea.Domain.Aggregates.ApplicationUserAggregate;
using Decenea.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Infrastructure.DataSeed;

public static class ApplicationRoleSeed
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<ApplicationRole>().HasData(
            new ApplicationRole()
        {
            Id = 1,
            Name = ApplicationRoles.SuperAdmin
        }, new ApplicationRole()
        {
            Id = 2,
            Name = ApplicationRoles.Admin
        }, new ApplicationRole()
        {
            Id = 3,
            Name = ApplicationRoles.Member
        }, new ApplicationRole()
        {
            Id = 4,
            Name = ApplicationRoles.Guest
        });
    }
}