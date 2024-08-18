using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.GroupAggregate;

public class Group : AuditableAggregateRoot
{
    public required string Name { get; set; }
    private readonly List<GroupMember> _groupMembers = new ();
    public IReadOnlyCollection<GroupMember> GroupUsers => _groupMembers.AsReadOnly();

}