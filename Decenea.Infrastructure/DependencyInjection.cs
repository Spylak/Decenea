using System.Text;
using Dapper;
using Decenea.Application.Abstractions.DataAccess;
using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Abstractions.Persistance.IRepositories;
using Decenea.Infrastructure.Outbox;
using Decenea.Infrastructure.Persistence;
using Decenea.Infrastructure.Persistence.DataAccess;
using Decenea.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quartz;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Decenea.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddPersistence(services, configuration);
        
        AddAuthentication(services, configuration);

        AddBackgroundJobs(services, configuration);
        
        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("DeceneaDbConnectionString") ??
            throw new ArgumentNullException(nameof(configuration));
        
        services.AddDbContext<DeceneaDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddScoped<IDeceneaDbContext, DeceneaDbContext>();
        
        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        
        services.AddTransient<ITestRepository, TestRepository>();

    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(o => o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                SecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Auth:JWTSigningKey"]!));

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
    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

        services.AddQuartz(options => { options.UseMicrosoftDependencyInjectionJobFactory(); });

        services.AddQuartzHostedService(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });
        
        services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
    }

    public static async Task ExecuteInfrastructureOnStartup(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DeceneaDbContext>();
        if ((await db.Database.GetPendingMigrationsAsync()).Any())
        {
            await db.Database.MigrateAsync();
        }
    }
}