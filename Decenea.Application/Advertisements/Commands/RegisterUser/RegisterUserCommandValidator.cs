using FastEndpoints;
using FluentValidation;

namespace Decenea.Application.Advertisements.Commands.RegisterUser;
//
// internal sealed class RegisterUserCommandValidator : Validator<RegisterUserCommand>
// {
//     public RegisterUserCommandValidator()
//     {
//         RuleFor(c => c.FirstName).NotEmpty()
//             .WithMessage("Your first name is required!");
//
//         RuleFor(c => c.LastName).NotEmpty()
//             .WithMessage("Your last name is required!");
//         
//         RuleFor(c => c.MiddleName).NotEmpty()
//             .WithMessage("Your middle name is required!");
//
//         RuleFor(c => c.Email).EmailAddress();
//
//         RuleFor(c => c.Password).NotEmpty()
//             .MinimumLength(8);
//     }
// }