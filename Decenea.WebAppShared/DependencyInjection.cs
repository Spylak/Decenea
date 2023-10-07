using Decenea.WebAppShared.Services;
using Decenea.WebAppShared.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Decenea.WebAppShared;

public static class DependencyInjection
{
    public static IServiceCollection AddWebAppShared(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IGlobalFunctionService,GlobalFunctionService>();
        services.AddTransient<ICookieService,CookieService>();

        return services;
    }
}