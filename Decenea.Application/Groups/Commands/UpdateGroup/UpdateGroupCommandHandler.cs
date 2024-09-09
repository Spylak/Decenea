using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using ErrorOr;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Group = Decenea.Domain.Aggregates.GroupAggregate.Group;

namespace Decenea.Application.Groups.Commands.UpdateGroup;

public class UpdateGroupCommandHandler : ICommandHandler<UpdateGroupCommand, ErrorOr<GroupDto>>
{    
    private readonly IDeceneaDbContext _dbContext;

    public UpdateGroupCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<GroupDto>> ExecuteAsync(UpdateGroupCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var group = await _dbContext
                .Set<Group>()
                .FirstOrDefaultAsync(i => i.Id == command.GroupId && i.GroupMembers.Any(j => j.GroupUserEmail == command.UserId && j.GroupRole == GroupRole.Owner), cancellationToken);
            
            if(group is null)
                return Error.NotFound(description: "Group not found.");
            
            _dbContext.ModifiedBy = command.UserId;
            group.Name = command.Name;
            _dbContext.Set<Group>().Update(group);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group.GroupToGroupDto();
        }
        catch (Exception ex)
        {
            Log.Error("Failed to UpdateGroup from request: {command} with error: {ex}", command, ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}