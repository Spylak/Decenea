using Decenea.WebAPI.Services.CommandServices;
using Decenea.WebAPI.Services.CommandServices.ICommandServices;
using Decenea.WebAPI.Services.QueryServices;
using Decenea.WebAPI.Services.QueryServices.IQueryServices;

namespace Decenea.WebAPI.ServiceCollections;

public static class ApplicationServices
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IApplicationUserCommandService, ApplicationUserCommandService>();
        builder.Services.AddTransient<IApplicationUserQueryService, ApplicationUserQueryService>();
        builder.Services.AddTransient<ILocationQueryService, LocationQueryService>();
        builder.Services.AddTransient<IAdvertisementQueryService, AdvertisementQueryService>();
        builder.Services.AddTransient<IAdvertisementCommandService, AdvertisementCommandService>();
    }
}