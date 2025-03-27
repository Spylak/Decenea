using Blazored.LocalStorage;
using Decenea.Common.Apis;
using Decenea.Common.Requests.User;
using ErrorOr;
using Decenea.WebApp.Abstractions;

namespace Decenea.WebApp.Services;

public class UserService : IUserService
{
    private readonly ILocalStorageService _localStorage;
    private readonly IUserApi _userApi;
    private readonly IAuthStateProvider _authStateProvider;
    public UserService(ILocalStorageService localStorage, IUserApi userApi, IAuthStateProvider authStateProvider)
    {
        _localStorage = localStorage;
        _userApi = userApi;
        _authStateProvider = authStateProvider;
    }
    public async  Task SetThemeLocalStorage(string theme)
    {
        await  _localStorage.SetItemAsync(SD.ThemeColor, theme);
    }

    public async Task<string> GetThemeLocalStorage()
    {
        var theme = await _localStorage.GetItemAsync<string>(SD.ThemeColor);
        return theme ?? string.Empty;
    }
    
    public async Task<ErrorOr<object>> RegisterUser(RegisterUserRequest user)
    {
        try
        {
            var response = await _userApi.Register(user);
            if (response.IsError)
                return Error.Failure(description:
                    response.Messages.FirstOrDefault().Value, metadata: response.Messages.ToDictionary(i=>i.Key, i => (object)i.Value));
            
            _authStateProvider.NotifyAuthenticationStateChanged();
            return true;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }
}