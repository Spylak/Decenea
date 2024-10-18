using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Enums;
using Decenea.Domain.Aggregates.GroupAggregate;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.Group.Commands.RemoveGroupMembers;

public class RemoveGroupMembersCommandHandler : ICommandHandler<RemoveGroupMembersCommand, ErrorOr<bool>>
{
    private readonly IDeceneaDbContext _dbContext;

    public RemoveGroupMembersCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ErrorOr<bool>> ExecuteAsync(RemoveGroupMembersCommand command, CancellationToken ct)
    {
        try
        {
            var groupMembers = await _dbContext
                .Set<GroupMember>()
                .Where(j => j.GroupId == command.GroupId 
                            && (command.GroupUserEmails.Contains(j.GroupUserEmail) || command.UserEmail == j.GroupUserEmail))
                .ToListAsync(ct);
            
            if (groupMembers.Count == 1)
            {
                return Error.Conflict(description: "Last group member.");
            }

            var groupMemberAccess = groupMembers.FirstOrDefault(i =>
                i.GroupUserEmail == command.UserEmail && i.GroupRole == GroupRole.Owner);
            
            if (groupMemberAccess is null)
            {
                return Error.Unauthorized(description: "You are not authorized to remove group members");
            }

            var groupMembersToRemove = groupMembers.Where(i => command.GroupUserEmails.Contains(i.GroupUserEmail));
            
            _dbContext.ModifiedBy = command.UserId;
            _dbContext.Set<GroupMember>().RemoveRange(groupMembersToRemove);
            await _dbContext.SaveChangesAsync(ct);
            return true;
        }
        catch (Exception ex)
        {
            Log.Error("Failed to RemoveGroupMember from request: {command} with error: {ex}", command, ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}