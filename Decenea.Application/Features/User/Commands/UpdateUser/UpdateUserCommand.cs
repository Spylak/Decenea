using Decenea.Application.Abstractions.Messaging;
using Decenea.Common.DataTransferObjects.User;
using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommand: UpdateCommand, ICommand<ErrorOr<UserDto>>
{
    public string Id { get; init; }
    public string Email { get; init; }
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string MiddleName { get; init; }
    public string PhoneNumber { get; init; }
};