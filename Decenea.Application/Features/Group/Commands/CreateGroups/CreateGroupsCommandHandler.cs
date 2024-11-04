using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.Group.Commands.CreateGroups;

public class CreateGroupsCommandHandler : ICommandHandler<CreateGroupsCommand, ErrorOr<List<GroupDto>>>
{    
    private readonly IDeceneaDbContext _dbContext;

    public CreateGroupsCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<List<GroupDto>>> ExecuteAsync(CreateGroupsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var groupsToAddList = new List<Domain.Aggregates.GroupAggregate.Group>();
            var groupsToUpdateList = await _dbContext
                .Set<Domain.Aggregates.GroupAggregate.Group>()
                .Where(i => command.GroupDtos.Select(j => j.Id).Contains(i.Id))
                .ToListAsync(cancellationToken);
            
            foreach (var group in command.GroupDtos)
            {
                var groupToUpdate = groupsToUpdateList
                    .FirstOrDefault(i => i.Id == group.Id);
                if (groupToUpdate != null)
                {
                    groupToUpdate.Name = group.Name;
                }
                else
                {
                    var groupToAdd = Domain.Aggregates.GroupAggregate.Group.Create(group.Name, group.Id);
                    groupToAdd.AddNewGroupMember(command.UserEmail, groupToAdd.Id, GroupRole.Owner);
                    groupsToAddList.Add(groupToAdd);
                }
            }

            if (groupsToAddList.Count == 0)
                return Error.Failure(description: "No groups to add");
            
            _dbContext.ModifiedBy = command.UserId;
            _dbContext.Set<Domain.Aggregates.GroupAggregate.Group>().UpdateRange(groupsToUpdateList);
            await _dbContext.Set<Domain.Aggregates.GroupAggregate.Group>().AddRangeAsync(groupsToAddList, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return groupsToAddList.Select(i => i.GroupToGroupDto(false)).ToList();
        }
        catch (Exception ex)
        {
            Log.Error("Failed to UpsertTestAnswers Group from request: {command} with error: {ex}", command,ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}