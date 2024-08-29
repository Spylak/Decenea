using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Group = Decenea.Domain.Aggregates.GroupAggregate.Group;

namespace Decenea.Application.Groups.Queries.GetGroup;

public class GetGroupQueryHandler : ICommandHandler<GetGroupQuery, Result<GroupDto, Exception>>
{
    private readonly IDeceneaDbContext _dbContext;

    public GetGroupQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<GroupDto, Exception>> ExecuteAsync(GetGroupQuery command, CancellationToken ct)
    {
        var group = await _dbContext
            .Set<Group>()
            .FirstOrDefaultAsync(i => i.Id == command.GroupId && i.GroupMembers.Any(j => j.GroupUserEmail == command.UserEmail), ct);
            
        if(group is null)
            return Result<GroupDto, Exception>.Anticipated(null, ["Group not found."]);
        
        return Result<GroupDto, Exception>.Anticipated(group.GroupToGroupDto());
    }
}