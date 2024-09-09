using Decenea.Application.Abstractions.Persistance;
using ErrorOr;
using Decenea.Common.Enums;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Group = Decenea.Domain.Aggregates.GroupAggregate.Group;

namespace Decenea.Application.Groups.Commands.DeleteGroup;

public class DeleteGroupCommandHandler : ICommandHandler<DeleteGroupCommand, ErrorOr<bool>>
{
    private readonly IDeceneaDbContext _dbContext;

    public DeleteGroupCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ErrorOr<bool>> ExecuteAsync(DeleteGroupCommand command, CancellationToken ct)
    {
        var group = await _dbContext
            .Set<Group>()
            .Where(i => i.Id == command.GroupId 
                        && i.GroupMembers.Any(j => j.GroupUserEmail == command.UserEmail && j.GroupRole == GroupRole.Owner))
            .ExecuteDeleteAsync(ct);
            
        return group == 1;
    }
}