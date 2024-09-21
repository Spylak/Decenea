using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.Group.Commands.UpdateGroup;

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
                .Set<Domain.Aggregates.GroupAggregate.Group>()
                .FirstOrDefaultAsync(i => i.Id == command.GroupId && i.GroupMembers.Any(j => j.GroupUserEmail == command.UserEmail && j.GroupRole == GroupRole.Owner), cancellationToken);
            
            if(group is null)
                return Error.NotFound(description: "Group not found.");
            
            _dbContext.ModifiedBy = command.UserId;
            group.Name = command.Name;
            _dbContext.Set<Domain.Aggregates.GroupAggregate.Group>().Update(group);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return group.GroupToGroupDto(false);
        }
        catch (Exception ex)
        {
            Log.Error("Failed to UpdateGroup from request: {command} with error: {ex}", command, ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}