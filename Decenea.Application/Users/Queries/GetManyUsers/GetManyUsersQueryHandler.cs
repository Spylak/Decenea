using Decenea.Application.Abstractions.Messaging;
using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Domain.Aggregates.UserAggregate;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Application.Users.Queries.GetManyUsers;

public class GetManyUsersQueryHandler : ICommandHandler<GetManyUsersQuery, Result<List<UserDto>, Exception>>
{
    private readonly IDeceneaDbContext _dbContext;

    public GetManyUsersQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<List<UserDto>, Exception>> ExecuteAsync(GetManyUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Set<User>()
            .Select(i => i.UserToUserDto(null))
            .ToListAsync(cancellationToken);
        
        return Result<List<UserDto>, Exception>.Anticipated(users);
    }
}