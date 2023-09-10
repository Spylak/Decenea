using Decenea.Application.Services.QueryServices.IQueryServices;
using Decenea.Domain.Aggregates.UserAggregate;
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
    
    public async Task<Result<List<User>, Exception>> GetManyApplicationUsers()
    {
        var users = await _dbContext.Users.ToListAsync();
        return Result<List<User>, Exception>.Anticipated(users);
    }
}