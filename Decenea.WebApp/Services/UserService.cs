using Blazored.LocalStorage;
using Decenea.WebApp.Services.IService;

namespace Decenea.WebApp.Services;

public class UserService : IUserService
{
    private readonly ILocalStorageService _localStorage;

    public UserService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    public async  Task SetThemeLocalStorage(string theme)
    {
        await  _localStorage.SetItemAsync(SD.ThemeColor, theme);
    }

    public async  Task<string> GetThemeLocalStorage()
    {
        var theme = await _localStorage.GetItemAsync<string>(SD.ThemeColor);
        return theme;
    }
}