using Decenea.Common.DataTransferObjects.Group;
using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Features.Group.Queries.GetGroups;

public class GetGroupsQuery : ICommand<ErrorOr<List<GroupDto>>>
{
    public required string UserEmail { get; set; }
}