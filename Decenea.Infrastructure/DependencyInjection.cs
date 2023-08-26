using Decenea.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Decenea.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //DbContext needs to be stateless to use ContextPool
        services.AddDbContextPool<DeceneaDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DeceneaDbConnectionString"));
        });
    }
}