using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.RemoveGroupMembers;

public class RemoveGroupMembersCommand : ICommand<ErrorOr<bool>>
{
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
    public required string GroupId { get; set; }
    public List<string> GroupUserEmails { get; set; } = [];
}