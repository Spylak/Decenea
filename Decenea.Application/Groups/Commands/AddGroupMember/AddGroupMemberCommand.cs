using Decenea.Common.Common;
using Decenea.Common.Enums;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.AddGroupMember;

public class AddGroupMemberCommand : ICommand<Result<bool, Exception>>
{
    public required string UserId { get; set; }
    public required string GroupId { get; set; }
    public required string GroupUserEmail { get; set; }
    public required GroupRole GroupRole { get; set; }
}