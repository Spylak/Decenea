using Microsoft.JSInterop;
using Decenea.WebApp.Services.IService;

namespace Decenea.WebApp.Services;

public class GlobalFunctionService : IGlobalFunctionService
{
    private readonly IJSRuntime _jsRuntime;
    public GlobalFunctionService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task ConsoleLog<T>(T obj) where  T :class
    {
        await _jsRuntime.InvokeVoidAsync("blazorExtensions.Log", obj);
    }
    public async void Reload()
    {
        await _jsRuntime.InvokeVoidAsync("location.reload");
        await _jsRuntime.InvokeVoidAsync("blazorExtensions.Home", "/");
    }

    public HttpClient CreateClient(string subDomain="")
    {
        var uri = subDomain == "" ? "https://master.gyxi.com" : $"https://{subDomain}.gyxi.com";
        // string uri = subDomain == "" ? "https://gyxi9-paris-engine.azurewebsites.net" : $"https://{subDomain}.gyxi.com";
        var client = new HttpClient();
        client.BaseAddress = new Uri(uri);
        return client;
    }
}