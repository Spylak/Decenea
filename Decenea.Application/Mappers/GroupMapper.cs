using Decenea.Common.DataTransferObjects.Group;
using Decenea.Domain.Aggregates.GroupAggregate;

namespace Decenea.Application.Mappers;

public static class GroupMapper
{
    public static GroupDto GroupToGroupDto(this Group group, GroupDto? groupDto = null)
    {
        groupDto ??= new GroupDto();
        groupDto.Name = group.Name;
        return groupDto;
    }
    
    public static GroupDto GroupToGroupDto(this Group group)
    {
        return new GroupDto()
        {
            Id = group.Id,
            Name = group.Name
        };
    }
}