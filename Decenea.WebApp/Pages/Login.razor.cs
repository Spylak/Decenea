using Decenea.Common.Requests.User;
using Decenea.WebApp.Components;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Decenea.WebApp.Pages;

public partial class Login
{
    private LoginUserRequest Request { get; init; }
    private MudForm form;
    private string authMessage = "The user is NOT authenticated.";

    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (AuthenticationState is not null)
        {
            var authState = await AuthenticationState;
            var user = authState?.User;

            if (user?.Identity is not null && user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/mytests");
            }
        }
    }
    
    private async Task Submit()
    {
        await form.Validate();

        if (form.IsValid)
        {
            var result = await AuthApi.Login(Request);
            if (result.IsError)
            {
                Snackbar.Add("Submitted!");
            }
        }
    }
}

public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    
}