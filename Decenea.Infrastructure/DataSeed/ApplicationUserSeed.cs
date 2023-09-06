using Decenea.Domain.Aggregates.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Infrastructure.DataSeed;

public static class ApplicationUserSeed
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<ApplicationRole>().HasData(
            new ApplicationUser()
            {
                FirstName = "Admin",
                Email = "admin@decenea.com",
                UserName = "admin@decenea.com",
                LastName = "Decenea",
                MiddleName = "D.",
                PhoneNumber = "",
                CreatedBy = "ApplicationUserSeed",
                ResidenceOf = "Decenea"
            });
    }
}