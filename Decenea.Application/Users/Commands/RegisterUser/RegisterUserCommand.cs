using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;


namespace Decenea.Application.Users.Commands.RegisterUser;

public record RegisterUserCommand(string Email,
    string UserName,
    string FirstName,
    string LastName,
    string MiddleName,
    string PhoneNumber,
    string Password);