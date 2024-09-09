using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Extensions;
using Decenea.Domain.Aggregates.TestAggregate;
using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Tests.Commands.UpdateTest;

public class UpdateTestCommandHandler : ICommandHandler<UpdateTestCommand, ErrorOr<TestDto>>
{
    private readonly IDeceneaDbContext _dbContext;

    public UpdateTestCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<TestDto>> ExecuteAsync(UpdateTestCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var existingTest = await _dbContext
                .Set<Test>()
                .FirstOrDefaultAsync(i => i.Id == command.Id, cancellationToken);

            if (existingTest is null)
                return Error.NotFound(description: "Test not found.");

            if (existingTest.Version != command.Version)
            {
                return ErrorOrExt.ConcurrencyError(existingTest.TestToTestDto());
            }

            Test.Update(existingTest, command.Title,
                command.Description,
                command.ContactEmail,
                command.ContactPhone);

            _dbContext.Set<Test>().Update(existingTest);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            if (result.IsError)
                return result.Errors;
                
            return existingTest.TestToTestDto();
        }
        catch (Exception ex)
        {
            Log.Error("Failed to UpdateTest from request: {command} with error: {ex}", command, ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}