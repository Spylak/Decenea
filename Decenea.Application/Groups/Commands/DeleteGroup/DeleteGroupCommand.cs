using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.DeleteGroup;

public class DeleteGroupCommand : ICommand<ErrorOr<bool>>
{
    public string GroupId { get; set; }
    public string UserEmail { get; set; }
}