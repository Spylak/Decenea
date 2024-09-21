using Decenea.Application.Abstractions.Messaging;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;
using ErrorOr;

namespace Decenea.Application.Groups.Commands.UpdateGroupMember;

public class UpdateGroupMemberCommand : ICommand<ErrorOr<GroupMemberDto>>
{
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
    public required string GroupId { get; set; }
    public UpdateGroupMemberDto? UpdateGroupMemberDto { get; set; }
}