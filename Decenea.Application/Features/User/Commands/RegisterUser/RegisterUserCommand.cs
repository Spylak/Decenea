using Decenea.Common.DataTransferObjects.User;
using FastEndpoints;

namespace Decenea.Application.Features.User.Commands.RegisterUser;

public record RegisterUserCommand(string Email,
    string UserName,
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    string Password) : ICommand<ErrorOr<UserDto>>;