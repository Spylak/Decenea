using ErrorOr;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;

namespace Decenea.Application.Groups.Queries.GetGroup;

public class GetGroupQuery : ICommand<ErrorOr<GroupDto>>
{
    public string GroupId { get; set; }
    public string UserEmail { get; set; }
}