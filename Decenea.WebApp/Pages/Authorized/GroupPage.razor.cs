using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Common.Requests.Group;
using Microsoft.AspNetCore.Components;

namespace Decenea.WebApp.Pages.Authorized;

public partial class GroupPage
{
    [Parameter] public string? GroupId { get; set; }
    private GroupDto Group { get; set; } = new ()
    {
        Name = "Initial"
    };

    protected override async Task OnInitializedAsync()
    {
        if (GroupId is not null)
        {
            var result  = await GroupApi.Get(new GetGroupRequest(GroupId));
            if (result is { IsError: false, Data: not null })
                Group = result.Data;
        }
    }

    private void AddGroupMember()
    {
        Group.GroupMembers.Add(new GroupMemberDto()
        {
            GroupId = Group.Id,
            GroupRole = GroupRole.Member,
            GroupUserEmail = "user@email.com"
        });
    }

    private async Task SaveChanges()
    {
        await GroupApi.AddMembers(new AddGroupMembersRequest()
        {
            GroupId = Group.Id,
            GroupMembers = Group.GroupMembers.Select(i => new AddGroupMemberDto()
            {
                GroupUserEmail = i.GroupUserEmail,
                GroupRole = i.GroupRole,
                Alias = i.Alias
            }).ToList()
        });
        if (GroupId is not null)
        {
            var result  = await GroupApi.Get(new GetGroupRequest(GroupId));
            if (result is { IsError: false, Data: not null })
                Group = result.Data;
            StateHasChanged();
        }
    }

    private async Task RemoveGroupMember(string userEmail)
    {
        var groupMember = Group.GroupMembers.FirstOrDefault(i => i.GroupUserEmail == userEmail);
        if (groupMember is null)
        {
            return;
        }
        Group.GroupMembers.Remove(groupMember);
        await GroupApi.RemoveMembers(new RemoveGroupMembersRequest()
        {
            GroupId = Group.Id,
            GroupUserEmails = [groupMember.GroupUserEmail]
        });
    }
    
    private async Task UpdateGroupMember(GroupMemberDto groupMemberDto)
    {
        if(groupMemberDto.Version is null)
            return;
        
        await GroupApi.UpdateMember(new UpdateGroupMemberRequest
        {
            GroupId = Group.Id,
            UpdateGroupMemberDto = new UpdateGroupMemberDto
            {
                Version = groupMemberDto.Version,
                GroupUserEmail = groupMemberDto.GroupUserEmail,
                GroupRole = groupMemberDto.GroupRole,
                Alias = groupMemberDto.Alias
            }
        });
    }
}