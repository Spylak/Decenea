using Decenea.Application.Abstractions.Messaging;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;

namespace Decenea.Application.Features.Group.Commands.UpdateGroup;

public class UpdateGroupCommand : UpdateCommand, ICommand<ErrorOr<GroupDto>>
{
    public string Name { get; set; }
    public required string GroupId { get; set; }
    public required string UserEmail { get; set; }
    public required string UserId { get; set; }
}