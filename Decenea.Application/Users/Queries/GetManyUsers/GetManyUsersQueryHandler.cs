using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Domain.Aggregates.UserAggregate;
using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Application.Users.Queries.GetManyUsers;

public class GetManyUsersQueryHandler : ICommandHandler<GetManyUsersQuery, ErrorOr<List<UserDto>>>
{
    private readonly IDeceneaDbContext _dbContext;

    public GetManyUsersQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ErrorOr<List<UserDto>>> ExecuteAsync(GetManyUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Set<User>()
            .Select(i => i.UserToUserDto(null))
            .ToListAsync(cancellationToken);
        
        return users;
    }
}