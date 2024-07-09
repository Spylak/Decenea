using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.GroupAggregate;

public class Group : AuditableAggregateRoot
{
    private readonly List<string> _userIds = new ();

}