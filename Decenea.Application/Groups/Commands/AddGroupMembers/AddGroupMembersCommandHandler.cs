using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Enums;
using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Group = Decenea.Domain.Aggregates.GroupAggregate.Group;

namespace Decenea.Application.Groups.Commands.AddGroupMembers;

public class AddGroupMembersCommandHandler : ICommandHandler<AddGroupMembersCommand, ErrorOr<bool>>
{    
    private readonly IDeceneaDbContext _dbContext;

    public AddGroupMembersCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<bool>> ExecuteAsync(AddGroupMembersCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (command.GroupMemberDtos.Count == 0)
                return Error.Failure(description: "No members to add.");
            
            var group = await _dbContext
                .Set<Group>()
                .FirstOrDefaultAsync(i => i.Id == command.GroupId && 
                                          i.GroupMembers.Any(j => j.GroupUserEmail == command.UserEmail && j.GroupRole == GroupRole.Owner), cancellationToken);
            
            if(group is null)
                return Error.NotFound(description: "Group not found.");
            
            _dbContext.ModifiedBy = command.UserId;
            foreach (var groupMember in command.GroupMemberDtos)
            {
                group.AddNewGroupMember(groupMember.GroupUserEmail, command.GroupId, groupMember.GroupRole);
            }
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