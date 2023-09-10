using Microsoft.EntityFrameworkCore;

namespace Decenea.Application.Abstractions.Persistance;

public interface IDeceneaDbContext
{
    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    string? CreatedBy { get; set; }
}