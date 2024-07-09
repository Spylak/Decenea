using Decenea.Domain.Common;

namespace Decenea.Domain.Aggregates.GroupAggregate;

public class GroupMember : AuditableEntity
{
    public string UserId { get; set; }
    public string GroupId { get; set; }
    public int GroupRoleId { get; set; }
}