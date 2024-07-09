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
                command.UserId,
                command.ContactEmail,
                command.ContactPhone);
            
            if(!createResult.IsSuccess || createResult.Value is null)
                return Result<TestDto, Exception>.Anticipated(null, createResult.Messages);

            await _dbContext.Set<Test>().AddAsync(createResult.Value, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result<TestDto, Exception>.Anticipated(createResult.Value.TestToTestDto(),"Successfully created!", true);
        }
        catch (Exception ex)
        {
            Log.Error("Failed to CreateTest from request: {command} with error: {ex}", command,ex);
            return Result<TestDto, Exception>.Excepted(ex);
        }
    }
}