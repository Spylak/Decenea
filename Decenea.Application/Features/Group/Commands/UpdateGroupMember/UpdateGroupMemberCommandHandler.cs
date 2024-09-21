using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Domain.Aggregates.GroupAggregate;
using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.Group.Commands.UpdateGroupMember;

public class UpdateGroupMemberCommandHandler : ICommandHandler<UpdateGroupMemberCommand, ErrorOr<GroupMemberDto>>
{
    private readonly IDeceneaDbContext _dbContext;

    public UpdateGroupMemberCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ErrorOr<GroupMemberDto>> ExecuteAsync(UpdateGroupMemberCommand command, CancellationToken ct)
    {
        try
        {
            if (command.UpdateGroupMemberDto is null)
            {
                return Error.Failure(description: "Group member to update not found.");
            }
            
            var groupMembers = await _dbContext
                .Set<GroupMember>()
                .Where(i => i.GroupId == command.GroupId  &&
                            (i.GroupUserEmail == command.UpdateGroupMemberDto.GroupUserEmail
                    && i.Version == command.UpdateGroupMemberDto.Version || i.GroupUserEmail == command.UserEmail))
                .ToListAsync(ct);
            
            if(groupMembers.Count == 0)
                return Error.NotFound(description: "Group members not found.");

            var commandCaller = groupMembers.FirstOrDefault(i => i.GroupUserEmail == command.UserEmail && i.GroupRole == GroupRole.Owner);

            if (commandCaller is null)
            {
                return Error.Failure(description: "Caller is not found.");
            }
            
            var groupMember = groupMembers.FirstOrDefault(i => i.GroupUserEmail == command.UpdateGroupMemberDto.GroupUserEmail
                                                               && i.Version == command.UpdateGroupMemberDto.Version);
            
            if (groupMember is null)
            {
                return Error.Failure(description: "Group member couldn't be updated.");
            }
            
            _dbContext.ModifiedBy = command.UserId;
            groupMember.Alias = command.UpdateGroupMemberDto.Alias;
            groupMember.GroupRole = command.UpdateGroupMemberDto.GroupRole;
            _dbContext.Set<GroupMember>().Update(groupMember);
            await _dbContext.SaveChangesAsync(ct);
            return groupMember.GroupMemberToGroupMemberDto();
        }
        catch (Exception ex)
        {
            Log.Error("Failed to Update Group Member from request: {command} with error: {ex}", command, ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}