using Decenea.WebAppShared.Abstractions;
using Decenea.WebAppShared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Decenea.WebAppShared;

public static class DependencyInjection
{
    public static IServiceCollection AddWebAppShared(this IServiceCollection services, IConfiguration configuration)
    {
        

        return services;
    }
}