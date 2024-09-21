using Decenea.Common.DataTransferObjects.Group;
using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.CreateGroups;

public class CreateGroupsCommand : ICommand<ErrorOr<List<GroupDto>>>
{
    public List<GroupDto> GroupDtos { get; set; } = new ();
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
}