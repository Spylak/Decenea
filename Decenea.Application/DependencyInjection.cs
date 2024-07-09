using Microsoft.Extensions.DependencyInjection;

namespace Decenea.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}