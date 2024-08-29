using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;

namespace Decenea.Application.Groups.Queries.GetGroup;

public class GetGroupQuery : ICommand<Result<GroupDto, Exception>>
{
    public string GroupId { get; set; }
    public string UserEmail { get; set; }
}