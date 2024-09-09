using Decenea.Common.Common;
using Decenea.Common.Enums;
using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.GroupAggregate;

public class Group : AuditableAggregateRoot
{
    public required string Name { get; set; }
    private readonly List<GroupMember> _groupMembers = new ();
    public IReadOnlyCollection<GroupMember> GroupMembers => _groupMembers.AsReadOnly();
    public static Group Create(string name)
    {
        var test = new Group()
        {
            Name = name
        };

        return test;
    }
    
    public void AddNewGroupMember(string userEmail, string groupId, GroupRole groupRole)
    {
        _groupMembers.Add(new GroupMember()
        {
            GroupRole = groupRole,
            GroupUserEmail = userEmail,
            GroupId = groupId
        });
    }
}