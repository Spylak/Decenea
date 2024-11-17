using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Features.Group.Commands.AddGroupMembers;
using Decenea.Common.Enums;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.Group.Commands.SyncTests;

public class SyncTestsCommandHandler : ICommandHandler<SyncTestsCommand, ErrorOr<bool>>
{    
    private readonly IDeceneaDbContext _dbContext;

    public SyncTestsCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<bool>> ExecuteAsync(SyncTestsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var group = await _dbContext
                .Set<Domain.Aggregates.GroupAggregate.Group>()
                .Include(i => i.TestGroups)
                .FirstOrDefaultAsync(i => i.Id == command.GroupId && 
                                          i.GroupMembers.Any(j => j.GroupUserEmail == command.UserEmail && j.GroupRole == GroupRole.Owner), cancellationToken);
            
            if(group is null)
                return Error.NotFound(description: "Group not found.");
            
            _dbContext.ModifiedBy = command.UserId;
            group.SyncTests(command.TestIds);
            _dbContext.Set<Domain.Aggregates.GroupAggregate.Group>().Update(group);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            Log.Error("Failed to AddGroupMember from request: {command} with error: {ex}", command, ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}