using Decenea.Application.Services.CommandServices;
using Decenea.Application.Services.CommandServices.ICommandServices;
using Decenea.Application.Services.QueryServices;
using Decenea.Application.Services.QueryServices.IQueryServices;

namespace Decenea.WebAPI.ServiceCollections;

public static class ApplicationServices
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IApplicationUserCommandService, ApplicationUserCommandService>();
        builder.Services.AddTransient<IApplicationUserQueryService, ApplicationUserQueryService>();
        builder.Services.AddTransient<ILocationQueryService, LocationQueryService>();
    }
}