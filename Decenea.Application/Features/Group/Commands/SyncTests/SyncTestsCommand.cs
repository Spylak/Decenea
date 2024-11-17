using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;

namespace Decenea.Application.Features.Group.Commands.SyncTests;

public class SyncTestsCommand : ICommand<ErrorOr<bool>>
{
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
    public required string GroupId { get; set; }
    public List<string> TestIds { get; set; } = [];
}