using Decenea.Common.Requests.User;
using ErrorOr;

namespace Decenea.WebApp.Abstractions;

public interface IUserService
{
    Task SetThemeLocalStorage(string theme);
    Task<string> GetThemeLocalStorage();
    Task<ErrorOr<object>> RegisterUser(RegisterUserRequest user);
}