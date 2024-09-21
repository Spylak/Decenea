using Decenea.WebApp.Abstractions;
using Microsoft.JSInterop;

namespace Decenea.WebApp.Services;

public class GlobalFunctionService : IGlobalFunctionService
{
    private readonly IJSRuntime _jsRuntime;
    public GlobalFunctionService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    public async Task ConsoleLogAsync(object obj)
    {
        await _jsRuntime.InvokeVoidAsync("blazorExtensions.Log", obj);
    }
    public async Task ReloadAsync()
    {
        await _jsRuntime.InvokeVoidAsync("location.reload");
        await _jsRuntime.InvokeVoidAsync("GlobalFunctions.Home", "/");
    }
    public async void Reload()
    {
        await _jsRuntime.InvokeVoidAsync("location.reload");
        await _jsRuntime.InvokeVoidAsync("blazorExtensions.Home", "/");
    }

    public HttpClient CreateClient(string subDomain = "")
    {
        var uri = subDomain == "" ? "http://localhost:5080" : $"http://{subDomain}.localhost:5080";
        var client = new HttpClient();
        client.BaseAddress = new Uri(uri);
        return client;
    }
}