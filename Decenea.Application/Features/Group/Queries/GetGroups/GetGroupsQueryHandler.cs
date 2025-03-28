using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Application.Features.Group.Queries.GetGroups;

public class GetGroupsQueryHandler : ICommandHandler<GetGroupsQuery, ErrorOr<List<GroupDto>>>
{
    private readonly IDeceneaDbContext _dbContext;

    public GetGroupsQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ErrorOr<List<GroupDto>>> ExecuteAsync(GetGroupsQuery command, CancellationToken ct)
    {
        var group = await _dbContext
            .Set<Domain.Aggregates.GroupAggregate.Group>()
            .Where(i => i.GroupMembers.Any(j => j.GroupUserEmail == command.UserEmail))
            .Select(i => i.GroupToGroupDto(false,true))
            .ToListAsync(ct);
            
        return group;
    }
}