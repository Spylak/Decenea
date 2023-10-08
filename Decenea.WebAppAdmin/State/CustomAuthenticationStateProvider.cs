using System.Security.Claims;
using Blazored.LocalStorage;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Extensions;
using Decenea.WebAppShared.Constants;
using Microsoft.AspNetCore.Components.Authorization;

namespace Decenea.WebAppAdmin.State;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private UserDto? _currentUser;
    public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await _localStorage.GetItemAsync<string>(GenericConstants.JwtToken);
            
            if (!string.IsNullOrWhiteSpace(token))
            {
                var claims = token.GetTokenClaims();
                if (!claims.IsSuccess)
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, "") })));
                }
                var identity = new ClaimsIdentity(claims.Value,"jwtAuthType");
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
        }
        catch (HttpRequestException ex)
        {
        }
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, "") })));
    }
    private async Task<Result<UserDto, Exception>> GetCurrentUser()
    {
        try
        {
            var jwtString = await _localStorage.GetItemAsStringAsync(GenericConstants.JwtToken);
            var jwtValidity = jwtString.IsJwtTokenExpired();
            if (!jwtValidity.IsSuccess)
            {
                await NotifyUserLogoutAsync();
                return Result<UserDto, Exception>.Anticipated(null,jwtValidity.Messages);
            }

            if (_currentUser != null)
                return Result<UserDto, Exception>.Anticipated(_currentUser);

            var userDto = await _localStorage.GetItemAsync<UserDto>(GenericConstants.User);
            if (userDto is null)
            {
                await NotifyUserLogoutAsync();
                return Result<UserDto, Exception>.Anticipated(null, "No user found.");
            }

            _currentUser = userDto;
            return Result<UserDto, Exception>.Anticipated(userDto);
        }
        catch (Exception ex)
        {
            await NotifyUserLogoutAsync();
            return Result<UserDto, Exception>.Excepted(ex);
        }
    }
    public async Task NotifyUserLoggedInAsync(string jwtString, UserDto userDto)
    {
        _currentUser = userDto;
        await _localStorage.SetItemAsync(GenericConstants.User, userDto);
        await _localStorage.SetItemAsync(GenericConstants.JwtToken, jwtString);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task NotifyUserLogoutAsync()
    {
        _currentUser = null;
        await _localStorage.RemoveItemAsync(GenericConstants.User);
        await _localStorage.RemoveItemAsync(GenericConstants.JwtToken);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}