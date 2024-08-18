using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Domain.Aggregates.TestAggregate;
using FastEndpoints;
using Serilog;

namespace Decenea.Application.Tests.Commands.CreateTest;

public class CreateTestCommandHandler : ICommandHandler<CreateTestCommand, Result<TestDto,Exception>>
{    
    private readonly IDeceneaDbContext _dbContext;

    public CreateTestCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<TestDto, Exception>> ExecuteAsync(CreateTestCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var createResult = Test.Create(command.Title,
                command.Description,
                command.ContactEmail,
                command.ContactPhone);
            
            if(!createResult.IsSuccess || createResult.SuccessValue is null)
                return Result<TestDto, Exception>.Anticipated(null, createResult.Messages);
            
            _dbContext.ModifiedBy = command.UserId;
            await _dbContext.Set<Test>().AddAsync(createResult.SuccessValue, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result<TestDto, Exception>.Anticipated(createResult.SuccessValue.TestToTestDto(),["Successfully created!"], true);
        }
        catch (Exception ex)
        {
            Log.Error("Failed to CreateTest from request: {command} with error: {ex}", command,ex);
            return Result<TestDto, Exception>.Excepted(ex);
        }
    }
}