using Decenea.Common.Common;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Application.Abstractions.Persistance;

public interface IDeceneaDbContext
{
    DbSet<T> Set<T>() where T : class;
    Task<Result<object,Exception>> SaveChangesAsync(string? createdBy = null, CancellationToken cancellationToken = default);
    Task<Result<object,Exception>> SaveChangesAsync(CancellationToken cancellationToken = default);
    string? CreatedBy { get; set; }
}