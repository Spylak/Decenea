using Decenea.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Decenea.Application.Abstractions.Persistance;

public interface IDeceneaDbContext
{
    DatabaseFacade Database { get; } 
    DbSet<T> Set<T>() where T : Versioned;
    Task<ErrorOr<object>> SaveChangesAsync(string? createdBy = null, CancellationToken cancellationToken = default);
    Task<ErrorOr<object>> SaveChangesAsync(CancellationToken cancellationToken = default);
    string? ModifiedBy { get; set; }
}