using Decenea.Application.Services.CommandServices;
using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Application.Services.QueryServices;
using Decenea.Application.Services.QueryServices.IQueryServices;
using Microsoft.Extensions.DependencyInjection;

namespace Decenea.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IApplicationUserCommandService, ApplicationUserCommandService>();
        services.AddTransient<IApplicationUserQueryService, ApplicationUserQueryService>();
        services.AddTransient<ILocationQueryService, LocationQueryService>();
        services.AddTransient<IAdvertisementQueryService, AdvertisementQueryService>();
        services.AddTransient<IAdvertisementCommandService, AdvertisementCommandService>();
    }
}