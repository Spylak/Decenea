using FastEndpoints;

namespace Decenea.Application.Features.Group.Commands.DeleteGroups;

public class DeleteGroupsCommand : ICommand<ErrorOr<bool>>
{
    public required List<string> GroupIds { get; set; }
    public required string UserEmail { get; set; }
}