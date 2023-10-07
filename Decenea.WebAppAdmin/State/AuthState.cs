using System.Security.Claims;
using Blazored.LocalStorage;
using Decenea.WebAppShared.Services.IServices;
using Microsoft.AspNetCore.Components.Authorization;

namespace Decenea.WebAppAdmin.State;

public class AuthState : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    // private User? _currentUser;
    private readonly ICookieService _cookieService;
    public AuthState(ILocalStorageService localStorage, ICookieService cookieService)
    {
        _localStorage = localStorage;
        _cookieService = cookieService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            // var userInfo = await GetCurrentUser();
            // if (!string.IsNullOrEmpty(userInfo.Email))
            // {
            //     var claims = new[] { new Claim(ClaimTypes.Email, userInfo.Email) };
            //     var identity = new ClaimsIdentity(claims,"jwtAuthType");
            //     return new AuthenticationState(new ClaimsPrincipal(identity));
            // }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Request failed:" + ex.Message);
        }
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, "") })));
    }
    // private async Task<User> GetCurrentUser()
    // {
    //     var token = await _authService.GetCookie(GenericConstants.GyxiToken);
    //     if (string.IsNullOrEmpty(token.Data))
    //     {
    //         return new User();
    //     }
    //     _currentUser= await  _localStorage.GetItemAsync<User>(GenericConstants.User);
    //     if (_currentUser != null && _currentUser.GyxiToken==token.Data ) return _currentUser;
    //     await _localStorage.RemoveItemAsync(GenericConstants.User);
    //     await _authService.DeleteCookie(GenericConstants.GyxiToken);
    //     NotifyUserLogout();
    //     return new User();
    // }
    public void NotifyUserLoggedIn()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}