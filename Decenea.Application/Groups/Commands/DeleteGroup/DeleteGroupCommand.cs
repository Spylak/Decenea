using Decenea.Common.Common;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.DeleteGroup;

public class DeleteGroupCommand : ICommand<Result<bool, Exception>>
{
    public string GroupId { get; set; }
    public string UserEmail { get; set; }
}