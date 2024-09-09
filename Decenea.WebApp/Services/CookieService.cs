using Decenea.Common.Common;
using Decenea.WebApp.Abstractions;
using ErrorOr;
using Microsoft.JSInterop;

namespace Decenea.WebApp.Services;

public class CookieService : ICookieService
{
    private IJSRuntime _jsRuntime;

    public CookieService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<ErrorOr<string>> SetCookieAsync(string name, string value)
    {
        try
        {
            var response = await _jsRuntime.InvokeAsync<string>("Methods.SetCookie", name, value, 3);
            return response;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }


    public async Task<ErrorOr<string>> GetCookieAsync(string name)
    {
        try
        {
            var response = await _jsRuntime.InvokeAsync<string>("Methods.GetCookie", name);
            return response;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }

    public async Task<ErrorOr<string>> DeleteCookieAsync(string name)
    {
        try
        {
            var response = await _jsRuntime.InvokeAsync<string>("Methods.DeleteCookie", name);
            return response;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }
}