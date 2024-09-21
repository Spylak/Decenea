using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using ErrorOr;
using FastEndpoints;
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
            foreach (var group in command.GroupDtos)
            {
                var groupToAdd = Domain.Aggregates.GroupAggregate.Group.Create(group.Name, group.Id);
                groupToAdd.AddNewGroupMember(command.UserEmail, groupToAdd.Id, GroupRole.Owner);
                groupsToAddList.Add(groupToAdd);
            }

            if (groupsToAddList.Count == 0)
                return Error.Failure(description: "No groups to add");
            
            _dbContext.ModifiedBy = command.UserId;
            await _dbContext.Set<Domain.Aggregates.GroupAggregate.Group>().AddRangeAsync(groupsToAddList, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return groupsToAddList.Select(i => i.GroupToGroupDto(false)).ToList();
        }
        catch (Exception ex)
        {
            Log.Error("Failed to Create Group from request: {command} with error: {ex}", command,ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}