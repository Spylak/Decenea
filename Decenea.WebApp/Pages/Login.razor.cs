using Decenea.Common.Requests.User;
using Decenea.WebApp.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Decenea.WebApp.Pages;

public partial class Login
{
    private LoginUserRequest Request { get; init; } = new ();
    private MudForm form;

    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (AuthenticationState is not null)
        {
            var authState = await AuthenticationState;
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo(Routes.Tests);
            }
        }
    }
    
    private async Task Submit()
    {
        await form.Validate();
        if (form.IsValid)
        {
            var result = await AuthService.Login(Request);
            if (!result.IsError)
            {
                Snackbar.Add("Submitted!");
                StateHasChanged();
            }
        }
    }
}

public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    
}