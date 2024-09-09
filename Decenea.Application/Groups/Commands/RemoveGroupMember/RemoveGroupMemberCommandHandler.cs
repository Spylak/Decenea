using Decenea.Application.Abstractions.Persistance;
using ErrorOr;
using Decenea.Domain.Aggregates.GroupAggregate;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Groups.Commands.RemoveGroupMember;

public class RemoveGroupMemberCommandHandler : ICommandHandler<RemoveGroupMemberCommand, ErrorOr<bool>>
{
    private readonly IDeceneaDbContext _dbContext;

    public RemoveGroupMemberCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ErrorOr<bool>> ExecuteAsync(RemoveGroupMemberCommand command, CancellationToken ct)
    {
        try
        {
            var groupMembers = await _dbContext
                .Set<GroupMember>()
                .Where(j => j.GroupId == command.GroupId && (command.UserEmail == j.GroupUserEmail
                                                             || command.GroupUserEmail == j.GroupUserEmail))
                .ToListAsync(ct);
            
            if (groupMembers.Count == 1)
            {
                return Error.Conflict(description: "Last group member.");
            }

            var groupMember = groupMembers.FirstOrDefault(i => i.GroupUserEmail == command.GroupUserEmail);
            if (groupMember is null)
            {
                return Error.NotFound(description: "No group member found.");
            }
            
            _dbContext.ModifiedBy = command.UserId;
            _dbContext.Set<GroupMember>().Remove(groupMember);
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