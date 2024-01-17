namespace Decenea.WebApp.Services.IService;

public interface IUserService
{
    Task SetThemeLocalStorage(string theme);
    Task<string> GetThemeLocalStorage();
}