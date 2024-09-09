using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.WebApp.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;

namespace Decenea.WebApp.State;

public class AuthStateProvider : AuthenticationStateProvider, IAuthStateProvider
{
    private readonly ILocalStorageService _localStorage;
    public string? AccessToken { get; private set; }
    public AuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var response = await _localStorage.GetItemAsync<LoginUserResponse>("session");
        if (response is not { AccessToken: not null })
        {
            return new(new(new ClaimsIdentity()));
        }

        AccessToken = response.AccessToken;

        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(response.AccessToken);
        var userIdentity = new ClaimsIdentity(jwtToken.Claims, "Basic");
        var principal = new ClaimsPrincipal(userIdentity);
        return new AuthenticationState(principal);
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}