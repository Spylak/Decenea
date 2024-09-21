using Decenea.Common.DataTransferObjects.Auth;

namespace Decenea.WebApp.Abstractions;

public interface IAuthStateProvider
{
    AuthTokensResponse AuthTokensResponse { get; }
    void NotifyAuthenticationStateChanged();
    Task NotifyUserLogin(AuthTokensResponse loginUserResponse);

    Task NotifyUserLogout();
}