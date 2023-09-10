using Decenea.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Infrastructure.DataSeed;

public static class ApplicationUserSeed
{
    public static void Seed(ModelBuilder builder)
    {
        // builder.Entity<User>().HasData(
        //     new User()
        //     {
        //         FirstName = "Admin",
        //         Email = "admin@decenea.com",
        //         UserName = "admin@decenea.com",
        //         LastName = "Decenea",
        //         MiddleName = "D.",
        //         PhoneNumber = ""
        //     });
    }
}