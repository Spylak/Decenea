using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using ErrorOr;
using FastEndpoints;
using Serilog;
using Group = Decenea.Domain.Aggregates.GroupAggregate.Group;

namespace Decenea.Application.Groups.Commands.CreateGroup;

public class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand, ErrorOr<GroupDto>>
{    
    private readonly IDeceneaDbContext _dbContext;

    public CreateGroupCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<GroupDto>> ExecuteAsync(CreateGroupCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var createResult = Group.Create(command.Name);
            
            _dbContext.ModifiedBy = command.UserId;
            createResult.AddNewGroupMember(command.UserId,createResult.Id, GroupRole.Owner);
            await _dbContext.Set<Group>().AddAsync(createResult, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return createResult.GroupToGroupDto();
        }
        catch (Exception ex)
        {
            Log.Error("Failed to Create Group from request: {command} with error: {ex}", command,ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}