using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;

namespace Decenea.Application.Groups.Queries.GetGroups;

public class GetGroupsQuery : ICommand<Result<List<GroupDto>, Exception>>
{
    public required string UserEmail { get; set; }
}