using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Group = Decenea.Domain.Aggregates.GroupAggregate.Group;

namespace Decenea.Application.Groups.Queries.GetGroups;

public class GetGroupsQueryHandler : ICommandHandler<GetGroupsQuery, Result<List<GroupDto>, Exception>>
{
    private readonly IDeceneaDbContext _dbContext;

    public GetGroupsQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<List<GroupDto>, Exception>> ExecuteAsync(GetGroupsQuery command, CancellationToken ct)
    {
        var group = await _dbContext
            .Set<Group>()
            .Where(i => i.GroupMembers.Any(j => j.GroupUserEmail == command.UserEmail))
            .Select(i => i.GroupToGroupDto())
            .ToListAsync(ct);
            
        return Result<List<GroupDto>, Exception>.Anticipated(group);
    }
}