using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Enums;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Application.Features.Group.Commands.DeleteGroups;

public class DeleteGroupsCommandHandler : ICommandHandler<DeleteGroupsCommand, ErrorOr<bool>>
{
    private readonly IDeceneaDbContext _dbContext;

    public DeleteGroupsCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ErrorOr<bool>> ExecuteAsync(DeleteGroupsCommand command, CancellationToken ct)
    {
        var group = await _dbContext
            .Set<Domain.Aggregates.GroupAggregate.Group>()
            .Where(i => command.GroupIds.Contains(i.Id)
                        && i.GroupMembers.Any(j => j.GroupUserEmail == command.UserEmail && j.GroupRole == GroupRole.Owner))
            .ExecuteDeleteAsync(ct);
            
        return group == 1;
    }
}