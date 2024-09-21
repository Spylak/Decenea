using Decenea.Common.DataTransferObjects.Group;
using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Features.Group.Commands.UpdateGroupMember;

public class UpdateGroupMemberCommand : ICommand<ErrorOr<GroupMemberDto>>
{
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
    public required string GroupId { get; set; }
    public UpdateGroupMemberDto? UpdateGroupMemberDto { get; set; }
}