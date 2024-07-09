using Decenea.Common.Common;
using Decenea.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Decenea.Application.Abstractions.Persistance;

public interface IDeceneaDbContext
{
    DatabaseFacade Database { get; } 
    DbSet<T> Set<T>() where T : Entity;
    Task<Result<object,Exception>> SaveChangesAsync(string? createdBy = null, CancellationToken cancellationToken = default);
    Task<Result<object,Exception>> SaveChangesAsync(CancellationToken cancellationToken = default);
    string? CreatedBy { get; set; }
}