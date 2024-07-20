using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.GroupAggregate;

public class Group : AuditableAggregateRoot
{
    private readonly List<GroupUser> _groupUsers = new ();
    public IReadOnlyCollection<GroupUser> GroupUsers => _groupUsers.AsReadOnly();

}