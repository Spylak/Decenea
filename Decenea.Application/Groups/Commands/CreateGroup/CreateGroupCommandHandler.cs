using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Enums;
using Decenea.Domain.Aggregates.GroupAggregate;
using FastEndpoints;
using Serilog;
using Group = Decenea.Domain.Aggregates.GroupAggregate.Group;

namespace Decenea.Application.Groups.Commands.CreateGroup;

public class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand, Result<GroupDto,Exception>>
{    
    private readonly IDeceneaDbContext _dbContext;

    public CreateGroupCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GroupDto, Exception>> ExecuteAsync(CreateGroupCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var createResult = Group.Create(command.Name);
            
            if(!createResult.IsSuccess || createResult.SuccessValue is null)
                return Result<GroupDto, Exception>.Anticipated(null, createResult.Messages);
            
            _dbContext.ModifiedBy = command.UserId;
            createResult.SuccessValue.AddNewGroupMember(command.UserId,createResult.SuccessValue.Id, GroupRole.Owner);
            await _dbContext.Set<Group>().AddAsync(createResult.SuccessValue, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result<GroupDto, Exception>.Anticipated(createResult.SuccessValue.GroupToGroupDto(),["Successfully created!"], true);
        }
        catch (Exception ex)
        {
            Log.Error("Failed to Create Group from request: {command} with error: {ex}", command,ex);
            return Result<GroupDto, Exception>.Excepted(ex);
        }
    }
}