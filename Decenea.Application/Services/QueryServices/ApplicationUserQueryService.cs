using Decenea.Application.Services.QueryServices.IQueryServices;
using Decenea.Domain.Aggregates.ApplicationUserAggregate;
using Decenea.Domain.Common;
using Decenea.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Application.Services.QueryServices;

public class ApplicationUserQueryService : IApplicationUserQueryService
{
    private readonly DeceneaDbContext _dbContext;
    
    public ApplicationUserQueryService(DeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<List<ApplicationUser>, Exception>> GetManyApplicationUsers()
    {
        var users = await _dbContext.Users.ToListAsync();
        return Result<List<ApplicationUser>, Exception>.Anticipated(users);
    }
}