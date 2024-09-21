using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Requests.Group;
using Decenea.WebApp.Models;

namespace Decenea.WebApp.Pages.Authorized;

public partial class GroupsPage
{
    private List<GroupDto> Groups { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var result = await GroupApi.Get();
        if (!result.IsError)
            Groups = result.Data ?? new List<GroupDto>();
    }

    private void AddGroup()
    {
        Groups.Add(new GroupDto()
        {
            Name = "New Group " + Groups.Count,
            Id = Ulid.NewUlid().ToString()
        });
    }

    private async Task SaveGroups()
    {
        var groupDtos = Groups.Select(i => new GroupDto()
        {
            Id = i.Id,
            Name = i.Name,
            
        }).ToList();
        await GroupApi.Create(new CreateGroupsRequest(groupDtos));
    }

    private async Task RemoveGroup(string id)
    {
        var index = Groups.FindIndex(i => i.Id == id);
        Groups.RemoveAt(index);
        await GroupApi.Delete(new DeleteGroupsRequest([id]));
    }

    private async Task UpdateGroup(string id, string name, string version)
    {
        await GroupApi.Update(new UpdateGroupRequest(id, name, version));
    }
}