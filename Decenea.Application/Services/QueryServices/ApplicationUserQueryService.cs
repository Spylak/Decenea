using Decenea.Application.Services.QueryServices.IQueryServices;
using Decenea.Domain.Common;
using Decenea.Domain.Entities.ApplicationUser;
using Decenea.Infrastructure.Data;
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
        return new Result<List<ApplicationUser>, Exception>(users);
    }
}