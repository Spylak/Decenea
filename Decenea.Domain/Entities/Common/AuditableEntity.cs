namespace Decenea.Domain.Entities.Common;

public class AuditableEntity : IAuditableEntity
{
    public string Id { get; set; } = Ulid.NewUlid().ToString()!;
    public string? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
}