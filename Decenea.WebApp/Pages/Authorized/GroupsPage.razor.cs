using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Requests.Group;

namespace Decenea.WebApp.Pages.Authorized;

public partial class GroupsPage
{
    private List<GroupDto> Groups { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var result = await GroupApi.Get();
        if (!result.IsError)
            Groups = result.Data ?? [];
    }

    private void AddGroup()
    {
        Groups.Add(new GroupDto()
        {
            Name = "New Group " + Groups.Count,
            Version = string.Empty,
            Id = Ulid.NewUlid().ToString()
        });
    }

    private async Task SaveGroups()
    {
        var groupDtos = Groups
            .Where(i => string.IsNullOrWhiteSpace(i.Version))
            .Select(i => new GroupDto()
        {
            Id = i.Id,
            Name = i.Name,
            Version = string.Empty
        }).ToList();
        await GroupApi.Create(new CreateGroupsRequest(groupDtos));
        var result = await GroupApi.Get();
        if (!result.IsError)
            Groups = result.Data ?? [];
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
        var result = await GroupApi.Get();
        if (!result.IsError)
            Groups = result.Data ?? [];
    }
}