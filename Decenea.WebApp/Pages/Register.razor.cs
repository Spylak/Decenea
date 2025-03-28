using Decenea.Common.Requests.User;
using Decenea.WebApp.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Severity = MudBlazor.Severity;

namespace Decenea.WebApp.Pages;

public partial class Register
{
    [Inject] private IUserService UserService { get; set; } = null!;
    private string ConfirmPassword { get; set; }
    private RegisterUserRequest Request { get; set; } = new();
    private MudForm form;
    private RegisterUserRequestValidator RegisterUserRequestValidator = new RegisterUserRequestValidator();
    
    private string? PasswordMatch(string arg)
    {
        if (Request.Password != arg)
            return "Passwords don't match";
        return null;
    }
    private async Task Submit()
    {
        await form.Validate();

        if (form.IsValid)
        {
            Snackbar.Add("Submitted!");
        }
        else
        {
            Snackbar.Add(form.Errors.First(), Severity.Error);
            return;
        }
        var result = await UserService.RegisterUser(Request);
        if (!result.IsError)
        {
            Snackbar.Add("Created Successfully!", Severity.Success);

        }
        else
        {
            Snackbar.Add(result.FirstError.Description, Severity.Error);
        }
    }
}

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    
}