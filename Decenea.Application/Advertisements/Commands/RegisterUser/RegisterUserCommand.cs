using Decenea.Domain.Common;
using Mediator;

namespace Decenea.Application.Advertisements.Commands.RegisterUser;

public record RegisterUserCommand(string Email,
    string UserName,
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    string CityId,
    string Password) : ICommand<Result<object, Exception>>;