using Decenea.Common.Common;
using Decenea.WebAppShared.Services.IServices;
using Microsoft.JSInterop;

namespace Decenea.WebAppShared.Services;

public class CookieService : ICookieService
{
    private IJSRuntime _jsRuntime;

    public CookieService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<Result<string, Exception>> SetCookieAsync(string name, string value)
    {
        try
        {
            var response = await _jsRuntime.InvokeAsync<string>("Methods.SetCookie", name, value, 3);
            return Result<string, Exception>.Anticipated(response);
        }
        catch (Exception ex)
        {
            return Result<string, Exception>.Excepted(ex);
        }
    }


    public async Task<Result<string, Exception>> GetCookieAsync(string name)
    {
        try
        {
            var response = await _jsRuntime.InvokeAsync<string>("Methods.GetCookie", name);
            return Result<string, Exception>.Anticipated(response);
        }
        catch (Exception ex)
        {
            return Result<string, Exception>.Excepted(ex);
        }
    }

    public async Task<Result<string, Exception>> DeleteCookieAsync(string name)
    {
        try
        {
            var response = await _jsRuntime.InvokeAsync<string>("Methods.DeleteCookie", name);
            return Result<string, Exception>.Anticipated(response);
        }
        catch (Exception ex)
        {
            return Result<string, Exception>.Excepted(ex);
        }
    }
}