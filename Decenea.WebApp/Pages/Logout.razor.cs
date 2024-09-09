using Decenea.WebApp.Abstractions;
using Microsoft.AspNetCore.Components;

namespace Decenea.WebApp.Pages;

public partial class Logout
{
    [Inject] private IUserService UserService { get; set; } = null!;
    [Inject]
    private NavigationManager? NavMan { get; set; }
    private async Task Yes()
    {
        StateHasChanged();
        NavMan!.NavigateTo("/");
    }
}