using Dapper;
using Decenea.Application.Abstractions.DataAccess;
using Decenea.Infrastructure.Outbox;
using Decenea.Infrastructure.Persistance;
using Decenea.Infrastructure.Persistance.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quartz;
using Microsoft.Extensions.DependencyInjection;

namespace Decenea.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddPersistence(services, configuration);
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("DeceneaDbConnectionString") ??
            throw new ArgumentNullException(nameof(configuration));
        
        //DbContext needs to be stateless to use ContextPool
        services.AddDbContextPool<DeceneaDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
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
}