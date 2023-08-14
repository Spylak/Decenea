using Decenea.Domain.Entities.ApplicationUser;
using Decenea.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Decenea.WebAPI.ServiceCollections;

public static class InfrastructureServices
{
    public static void AddInfrastructureServices(this WebApplicationBuilder builder)
    {
        //DbContext needs to be stateless to use ContextPool
        builder.Services.AddDbContextPool<DeceneaDbContext>(options => 
            options.UseNpgsql(builder.Configuration.GetConnectionString("DeceneaDbConnectionString")));
        
        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Stores.MaxLengthForKeys = 128;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(24);
            })
            .AddEntityFrameworkStores<DeceneaDbContext>()
            .AddRoles<ApplicationRole>()
            .AddRoleManager<RoleManager<ApplicationRole>>()
            .AddDefaultTokenProviders();

    }
}