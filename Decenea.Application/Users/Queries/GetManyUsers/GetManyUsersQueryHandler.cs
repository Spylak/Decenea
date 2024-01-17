using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Common;
using Decenea.Domain.Aggregates.UserAggregate;

using Microsoft.EntityFrameworkCore;

namespace Decenea.Application.Users.Queries.GetManyUsers;

public class GetManyUsersQueryHandler
{
    private readonly IDeceneaDbContext _dbContext;

    public GetManyUsersQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async ValueTask<Result<List<User>, Exception>> Handle(GetManyUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Set<User>().ToListAsync();
        return Result<List<User>, Exception>.Anticipated(users);
    }
}