using Decenea.WebAppShared.Abstractions;
using Microsoft.JSInterop;

namespace Decenea.WebAppShared.Services;

public class GlobalFunctionService : IGlobalFunctionService
{
    private readonly IJSRuntime _jsRuntime;
    public GlobalFunctionService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task ConsoleLogAsync<T>(T obj) where  T : class
    {
        await _jsRuntime.InvokeVoidAsync("GlobalFunctions.Log", obj);
    }
    public async Task ReloadAsync()
    {
        await _jsRuntime.InvokeVoidAsync("location.reload");
        await _jsRuntime.InvokeVoidAsync("GlobalFunctions.Home", "/");
    }
}