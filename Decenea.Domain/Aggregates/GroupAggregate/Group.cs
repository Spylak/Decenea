using Decenea.Common.Enums;
using Decenea.Domain.Common;
using Decenea.Domain.Helpers;

namespace Decenea.Domain.Aggregates.GroupAggregate;

public class Group : AuditableAggregateRoot
{
    public Group(string? id)
    {
        Id = id ?? Ulid.NewUlid().ToString();
    }
    public required string Name { get; set; }
    private readonly List<GroupMember> _groupMembers = new ();
    public IReadOnlyCollection<GroupMember> GroupMembers => _groupMembers.AsReadOnly();
    public static Group Create(string name, string? id = null)
    {
        var test = new Group(id)
        {
            Name = name
        };

        return test;
    }
    
    public void AddNewGroupMember(string userEmail, string groupId, GroupRole groupRole, string? alias = null)
    {
        _groupMembers.Add(new GroupMember()
        {
            GroupRole = groupRole,
            GroupUserEmail = userEmail,
            GroupId = groupId,
            Alias = alias
        });
    }
}