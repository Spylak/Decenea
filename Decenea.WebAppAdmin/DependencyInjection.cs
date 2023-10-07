using Decenea.WebAppAdmin.State;
using Microsoft.AspNetCore.Components.Authorization;

namespace Decenea.WebAppAdmin;

public static class DependencyInjection
{
    public static IServiceCollection AddState(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AppState>();
        services.AddScoped<AuthState>();
        services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthState>());

        return services;
    }
}