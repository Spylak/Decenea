using System.Text;
using Decenea.Domain.Aggregates.ApplicationUserAggregate;
using Decenea.Infrastructure.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Decenea.WebAPI.ServiceCollections;

public static class AuthServices
{
    public static void AddAuthServices(this WebApplicationBuilder builder)
    {
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
        
        builder.Services
            .AddAuthentication(o => o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                SecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWTSigningKey"]!));

                //set defaults
                o.TokenValidationParameters.IssuerSigningKey = key;
                o.TokenValidationParameters.ValidateIssuerSigningKey = true;
                o.TokenValidationParameters.ValidateLifetime = true;
                o.TokenValidationParameters.ClockSkew = TimeSpan.FromSeconds(60);
                o.TokenValidationParameters.ValidAudience = null;
                o.TokenValidationParameters.ValidateAudience = false;
                o.TokenValidationParameters.ValidIssuer = null;
                o.TokenValidationParameters.ValidateIssuer = false;
                
                //correct any user mistake
                o.TokenValidationParameters.ValidateAudience = o.TokenValidationParameters.ValidAudience is not null;
                o.TokenValidationParameters.ValidateIssuer = o.TokenValidationParameters.ValidIssuer is not null;
            });
    }
}