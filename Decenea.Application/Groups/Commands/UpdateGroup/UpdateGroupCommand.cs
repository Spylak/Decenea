using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.UpdateGroup;

public class UpdateGroupCommand : ICommand<Result<GroupDto, Exception>>
{
    public string Name { get; set; }
    public required string GroupId { get; set; }
    public required string UserId { get; set; }
}