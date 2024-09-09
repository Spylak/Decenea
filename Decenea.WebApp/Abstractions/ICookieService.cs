using ErrorOr;

namespace Decenea.WebApp.Abstractions;

public interface ICookieService
{
    Task<ErrorOr<string>> SetCookieAsync(string name, string value);
    Task<ErrorOr<string>> GetCookieAsync(string name);
    Task<ErrorOr<string>> DeleteCookieAsync(string name);
}