using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Groups.Commands.DeleteGroups;

public class DeleteGroupsCommand : ICommand<ErrorOr<bool>>
{
    public List<string> GroupIds { get; set; }
    public string UserEmail { get; set; }
}