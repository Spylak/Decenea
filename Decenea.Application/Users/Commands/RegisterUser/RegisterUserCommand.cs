using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Mediator;

namespace Decenea.Application.Users.Commands.RegisterUser;

public record RegisterUserCommand(string Email,
    string UserName,
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    string CityId,
    string Password) : ICommand<Result<UserDto, Exception>>;