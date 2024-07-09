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

    
}