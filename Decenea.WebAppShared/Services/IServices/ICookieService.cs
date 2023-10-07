using Decenea.Common.Common;

namespace Decenea.WebAppShared.Services.IServices;

public interface ICookieService
{
    Task<Result<string, Exception>> SetCookieAsync(string name, string value);
    Task<Result<string, Exception>> GetCookieAsync(string name);
    Task<Result<string, Exception>> DeleteCookieAsync(string name);
}