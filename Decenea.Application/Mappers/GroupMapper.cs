using Decenea.Common.DataTransferObjects.Group;
using Decenea.Domain.Aggregates.GroupAggregate;

namespace Decenea.Application.Mappers;

public static class GroupMapper
{
    public static GroupDto GroupToGroupDto(this Group group, bool includeMembers)
    {
        return new GroupDto()
        {
            Id = group.Id,
            Name = group.Name,
            Version = group.Version,
            GroupMembers = group.GroupMembers.Select(i=>i.GroupMemberToGroupMemberDto()).ToList()
        };
    }
    
    public static GroupMemberDto GroupMemberToGroupMemberDto(this GroupMember groupMember)
    {
        return new GroupMemberDto()
        {
            GroupUserEmail = groupMember.GroupUserEmail,
            Alias = groupMember.Alias,
            Version = groupMember.Version,
            GroupId = groupMember.GroupId,
            GroupRole = groupMember.GroupRole
        };
    }
}