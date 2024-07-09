using Decenea.Common.Common;

namespace Decenea.WebApp.Services.IService;

public interface ICookieService
{
    Task<Result<string, Exception>> SetCookieAsync(string name, string value);
    Task<Result<string, Exception>> GetCookieAsync(string name);
    Task<Result<string, Exception>> DeleteCookieAsync(string name);
}