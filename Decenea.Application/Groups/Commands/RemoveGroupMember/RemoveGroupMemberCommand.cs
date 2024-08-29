using Decenea.Common.Common;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.RemoveGroupMember;

public class RemoveGroupMemberCommand : ICommand<Result<bool,Exception>>
{
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
    public required string GroupId { get; set; }
    public required string GroupUserEmail { get; set; }
}