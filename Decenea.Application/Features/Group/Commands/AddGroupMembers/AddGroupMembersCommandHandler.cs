using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Enums;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.Group.Commands.AddGroupMembers;

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
                .Set<Domain.Aggregates.GroupAggregate.Group>()
                .Include(i => i.GroupMembers)
                .FirstOrDefaultAsync(i => i.Id == command.GroupId && 
                                          i.GroupMembers.Any(j => j.GroupUserEmail == command.UserEmail && j.GroupRole == GroupRole.Owner), cancellationToken);
            
            if(group is null)
                return Error.NotFound(description: "Group not found.");
            
            _dbContext.ModifiedBy = command.UserId;
            
            var membersToUpdateList = group.GroupMembers
                .Where(i => command.GroupMemberDtos
                    .Select(j => j.GroupUserEmail)
                    .Contains(i.GroupUserEmail)).ToList();
            
            foreach (var groupMember in command.GroupMemberDtos)
            {
                var member = membersToUpdateList
                    .FirstOrDefault(i => i.GroupUserEmail == groupMember.GroupUserEmail);

                if (member is not null)
                {
                     member.GroupRole = groupMember.GroupRole;
                     member.Alias = groupMember.Alias;
                }
                else
                {
                    group.AddNewGroupMember(groupMember.GroupUserEmail, command.GroupId, groupMember.GroupRole);
                }
            }
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