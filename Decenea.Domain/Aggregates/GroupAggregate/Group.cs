using Decenea.Common.Enums;
using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Domain.Common;

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
    private List<TestGroup> _testGroups = new ();
    public IReadOnlyCollection<TestGroup> TestGroups  => _testGroups.AsReadOnly();
    public static Group Create(string name, string? id = null)
    {
        var group = new Group(id)
        {
            Name = name
        };

        return group;
    }
    
    public void SyncTests(List<string> testIds)
    {
        _testGroups = testIds.Select(i => new TestGroup()
        {
            TestId = i,
            GroupId = this.Id
        }).ToList();
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