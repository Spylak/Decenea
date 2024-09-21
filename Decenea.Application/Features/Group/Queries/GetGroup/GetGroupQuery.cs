using Decenea.Common.DataTransferObjects.Group;
using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Features.Group.Queries.GetGroup;

public class GetGroupQuery : ICommand<ErrorOr<GroupDto>>
{
    public string GroupId { get; set; }
    public string UserEmail { get; set; }
}