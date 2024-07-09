using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Domain.Aggregates.TestAggregate;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Tests.Commands.UpdateTest;

public class UpdateTestCommandHandler : ICommandHandler<UpdateTestCommand, Result<TestDto,Exception>>
{
    private readonly IDeceneaDbContext _dbContext;

    public UpdateTestCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<TestDto, Exception>> ExecuteAsync(UpdateTestCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var existingTest = await _dbContext
                .Set<Test>()
                .FirstOrDefaultAsync(i => i.Id == command.Id, cancellationToken);

            if (existingTest is null)
                return Result<TestDto, Exception>.Anticipated(null, "Test not found.");

            if (existingTest.Version != command.Version)
            {
                return Result<TestDto, Exception>.Anticipated(existingTest.TestToTestDto(), "Concurrency issue.", false);
            }

            var updateResult = Test.Update(existingTest, command.Title,
                command.Description,
                command.ContactEmail,
                command.ContactPhone);

            if (!updateResult.IsSuccess)
                return Result<TestDto, Exception>.Anticipated(null, updateResult.Messages);

            _dbContext.Set<Test>().Update(existingTest);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            if(!result.IsSuccess)
                return Result<TestDto, Exception>.Anticipated(null, result.Messages);
                
            return Result<TestDto, Exception>.Anticipated(existingTest.TestToTestDto(), "Successfully updated entity.", true);
        }
        catch (Exception ex)
        {
            Log.Error("Failed to UpdateTest from request: {command} with error: {ex}", command, ex);
            return Result<TestDto, Exception>.Excepted(ex);
        }
    }
}