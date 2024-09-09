using ErrorOr;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;

namespace Decenea.Application.Groups.Queries.GetGroups;

public class GetGroupsQuery : ICommand<ErrorOr<List<GroupDto>>>
{
    public required string UserEmail { get; set; }
}