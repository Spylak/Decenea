using Decenea.Application.Abstractions.Persistance;
using ErrorOr;
using Decenea.Common.Enums;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Group = Decenea.Domain.Aggregates.GroupAggregate.Group;

namespace Decenea.Application.Groups.Commands.AddGroupMember;

public class AddGroupMemberCommandHandler : ICommandHandler<AddGroupMemberCommand, ErrorOr<bool>>
{    
    private readonly IDeceneaDbContext _dbContext;

    public AddGroupMemberCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<bool>> ExecuteAsync(AddGroupMemberCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var group = await _dbContext
                .Set<Group>()
                .FirstOrDefaultAsync(i => i.Id == command.GroupId && i.GroupMembers.Any(j => j.GroupUserEmail == command.GroupUserEmail && j.GroupRole == GroupRole.Owner), cancellationToken);
            
            if(group is null)
                return Error.NotFound(description: "Group not found.");
            
            _dbContext.ModifiedBy = command.UserId;
            group.AddNewGroupMember(command.GroupUserEmail, command.GroupId, command.GroupRole);
            _dbContext.Set<Group>().Update(group);
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