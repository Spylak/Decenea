using Decenea.Application.Abstractions.Messaging;
using ErrorOr;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.UpdateGroup;

public class UpdateGroupCommand : UpdateCommand, ICommand<ErrorOr<GroupDto>>
{
    public string Name { get; set; }
    public required string GroupId { get; set; }
    public required string UserId { get; set; }
}