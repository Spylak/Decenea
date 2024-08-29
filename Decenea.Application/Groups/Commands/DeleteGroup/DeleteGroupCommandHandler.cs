using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Common;
using Decenea.Common.Enums;
using Decenea.Domain.Aggregates.GroupAggregate;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Group = Decenea.Domain.Aggregates.GroupAggregate.Group;

namespace Decenea.Application.Groups.Commands.DeleteGroup;

public class DeleteGroupCommandHandler : ICommandHandler<DeleteGroupCommand, Result<bool, Exception>>
{
    private readonly IDeceneaDbContext _dbContext;

    public DeleteGroupCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<bool, Exception>> ExecuteAsync(DeleteGroupCommand command, CancellationToken ct)
    {
        var group = await _dbContext
            .Set<Group>()
            .Where(i => i.Id == command.GroupId 
                        && i.GroupMembers.Any(j => j.GroupUserEmail == command.UserEmail && j.GroupRole == GroupRole.Owner))
            .ExecuteDeleteAsync(ct);
            
        return Result<bool, Exception>.Anticipated(group == 1);
    }
}