using Decenea.Common.Requests.User;
using Decenea.WebApp.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Decenea.WebApp.Pages;

public partial class Register
{
    [Inject] private IUserService UserService { get; set; } = null!;
    private string ConfirmPassword { get; set; }
    private RegisterUserRequest Request { get; set; } = new();
    private MudForm form;
    private RegisterUserRequestValidator RegisterUserRequestValidator = new RegisterUserRequestValidator();

    private async Task Submit()
    {
        await form.Validate();

        if (form.IsValid)
        {
            Snackbar.Add("Submitted!");
        }
    }
}

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    
}