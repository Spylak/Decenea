using Decenea.Common.DataTransferObjects.Group;
using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.AddGroupMembers;

public class AddGroupMembersCommand : ICommand<ErrorOr<bool>>
{
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
    public required string GroupId { get; set; }
    public List<AddGroupMemberDto> GroupMemberDtos { get; set; } = [];
}