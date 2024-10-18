namespace Decenea.WebApp.Pages;

public partial class Logout
{
    private async Task Yes()
    {
        await AuthService.UserLogout();
        NavigationManager.NavigateTo("/");
    }
}