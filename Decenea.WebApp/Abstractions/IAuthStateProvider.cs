namespace Decenea.WebApp.Abstractions;

public interface IAuthStateProvider
{
    string? AccessToken { get; }
    void NotifyAuthenticationStateChanged();
}