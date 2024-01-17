using Decenea.Application.Abstractions.Messaging;


namespace Decenea.Application.Users.Commands.UpdateUser;

public class UpdateUserCommand: UpdateCommand
{
    public string Id { get; init; }
    public string Email { get; init; }
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string MiddleName { get; init; }
    public string PhoneNumber { get; init; }
    public string CityId { get; init; }
};