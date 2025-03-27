using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.WebApp.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;

namespace Decenea.WebApp.State;

public class AuthStateProvider : AuthenticationStateProvider, IAuthStateProvider
{
    public string UserEmailRole { get; set; } = string.Empty;
    private readonly ILocalStorageService _localStorage;
    public AuthTokensResponse AuthTokensResponse { get; private set; } = new ();
    public AuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var response = await _localStorage.GetItemAsync<AuthTokensResponse>("session");
        if (response is not { AccessToken: not null })
        {
            return new(new(new ClaimsIdentity()));
        }

        AuthTokensResponse = response;
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(response.AccessToken);
        var userIdentity = new ClaimsIdentity(jwtToken.Claims, "Basic");
        var principal = new ClaimsPrincipal(userIdentity);
        var userEmail = principal.FindFirst(ClaimTypes.Email)?.Value;
        var userRole = principal.FindFirst("role")?.Value;
        UserEmailRole = $"{userEmail} - {userRole}";
        return new AuthenticationState(principal);
    }
    
    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task NotifyUserLogin(AuthTokensResponse loginUserResponse)
    {
        await _localStorage.SetItemAsync("session", loginUserResponse);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task NotifyUserLogout()
    {
        await _localStorage.RemoveItemAsync("session");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}