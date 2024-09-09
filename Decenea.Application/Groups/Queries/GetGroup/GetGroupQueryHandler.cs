using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using ErrorOr;
using Decenea.Common.DataTransferObjects.Group;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Group = Decenea.Domain.Aggregates.GroupAggregate.Group;

namespace Decenea.Application.Groups.Queries.GetGroup;

public class GetGroupQueryHandler : ICommandHandler<GetGroupQuery, ErrorOr<GroupDto>>
{
    private readonly IDeceneaDbContext _dbContext;

    public GetGroupQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ErrorOr<GroupDto>> ExecuteAsync(GetGroupQuery command, CancellationToken ct)
    {
        var group = await _dbContext
            .Set<Group>()
            .FirstOrDefaultAsync(i => i.Id == command.GroupId && i.GroupMembers.Any(j => j.GroupUserEmail == command.UserEmail), ct);
            
        if(group is null)
            return Error.NotFound(description: "Group not found.");
        
        return group.GroupToGroupDto();
    }
}