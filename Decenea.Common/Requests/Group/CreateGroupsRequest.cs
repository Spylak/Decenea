using Decenea.Common.DataTransferObjects.Group;

namespace Decenea.Common.Requests.Group;

public record CreateGroupsRequest(List<GroupDto> Groups);