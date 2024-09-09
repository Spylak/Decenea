using ErrorOr;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.CreateGroup;

public class CreateGroupCommand : ICommand<ErrorOr<GroupDto>>
{
    public string Name { get; set; }
    public string UserId { get; set; }
}