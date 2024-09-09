using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.RemoveGroupMember;

public class RemoveGroupMemberCommand : ICommand<ErrorOr<bool>>
{
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
    public required string GroupId { get; set; }
    public required string GroupUserEmail { get; set; }
}