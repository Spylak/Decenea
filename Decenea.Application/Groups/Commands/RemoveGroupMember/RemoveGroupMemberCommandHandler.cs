using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Common;
using Decenea.Common.Enums;
using Decenea.Domain.Aggregates.GroupAggregate;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Group = Decenea.Domain.Aggregates.GroupAggregate.Group;

namespace Decenea.Application.Groups.Commands.RemoveGroupMember;

public class RemoveGroupMemberCommandHandler : ICommandHandler<RemoveGroupMemberCommand, Result<bool,Exception>>
{
    private readonly IDeceneaDbContext _dbContext;

    public RemoveGroupMemberCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<bool, Exception>> ExecuteAsync(RemoveGroupMemberCommand command, CancellationToken ct)
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
                return Result<bool, Exception>.Anticipated(false, ["Last group member."]);
            }

            var groupMember = groupMembers.FirstOrDefault(i => i.GroupUserEmail == command.GroupUserEmail);
            if (groupMember is null)
            {
                return Result<bool, Exception>.Anticipated(false, ["No group member found."]);
            }
            
            _dbContext.ModifiedBy = command.UserId;
            _dbContext.Set<GroupMember>().Remove(groupMember);
            await _dbContext.SaveChangesAsync(ct);
            return Result<bool, Exception>.Anticipated(false ,["Successfully removed group member!"], true);
        }
        catch (Exception ex)
        {
            Log.Error("Failed to RemoveGroupMember from request: {command} with error: {ex}", command, ex);
            return Result<bool, Exception>.Excepted(ex);
        }
    }
}