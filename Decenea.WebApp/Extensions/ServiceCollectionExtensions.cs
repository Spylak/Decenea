using Blazored.LocalStorage;
using Decenea.WebApp.Apis;
using Decenea.WebApp.Middleware;
using Decenea.WebApp.State;

namespace Decenea.WebApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static IHttpClientBuilder AddTokenHandler(this IHttpClientBuilder httpClientBuilder)
    {
        httpClientBuilder.AddHttpMessageHandler(sp =>
        {
            var authApi = sp.GetRequiredService<IAuthApi>();
            var authStateProvider = sp.GetRequiredService<AuthStateProvider>();
            return new TokenHandler(authApi, authStateProvider);
        });
        return httpClientBuilder;
    }
}