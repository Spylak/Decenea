using Decenea.Domain.Common;
using Decenea.Domain.Constants;
using Decenea.Domain.Entities.ApplicationUser;
using Decenea.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Decenea.Infrastructure.DataSeed;

public static class ApplicationUserSeed
{
    public static async Task Seed(DeceneaDbContext dbContext,UserManager<ApplicationUser> userManager)
    {
        using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var user = new ApplicationUser()
            {
                FirstName = "Admin",
                Email = "admin@decenea.com",
                UserName = "admin@decenea.com",
                LastName = "Decenea",
                MiddleName = "D.",
                PhoneNumber = "",
                CreatedBy = "ApplicationUserSeed",
                ResidenceOf = "Decenea"
            };
            if (await userManager.FindByEmailAsync(user.Email) is null)
            {
                await userManager.CreateAsync(user, "Admin123$!");
                var registration = await userManager
                    .CreateAsync(user, "Admin123!");
            
                if (!registration.Succeeded)
                {
                    await transaction.RollbackAsync();
                }

                var roleResult = await userManager
                    .AddToRoleAsync(user, ApplicationRoles.SuperAdmin);
            
                if (!roleResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                }

                await userManager.SetLockoutEnabledAsync(user, true);
            }
           
            
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
    }
}