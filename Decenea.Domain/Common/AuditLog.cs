namespace Decenea.Domain.Common;

public class AuditLog : EntityVersion
{
    public required string EntityType { get; set; }
}