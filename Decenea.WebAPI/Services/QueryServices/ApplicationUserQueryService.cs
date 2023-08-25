using Decenea.WebAPI.Services.QueryServices.IQueryServices;
using Decenea.WebAPI.Domain.Common;
using Decenea.Domain.Entities.ApplicationUserEntities;
using Decenea.WebAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Decenea.WebAPI.Services.QueryServices;

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