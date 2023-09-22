using Decenea.Domain.Common.Enums;

namespace Decenea.Domain.Common;

public class EntityVersion : Entity
{
    public required string EntityId { get; set; }
    public ExecutedOperation ExecutedOperation { get; set; }
    public string? DataAfterExecutedOperation { get; set; }
    public DateTime OperationExecutedAt { get; set; }
}