using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.CreateGroup;

public class CreateGroupCommand : ICommand<Result<GroupDto, Exception>>
{
    public string Name { get; set; }
    public string UserId { get; set; }
}