using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Extensions;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.Test.Commands.UpdateTest;

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
                .Set<Domain.Aggregates.TestAggregate.Test>()
                .Include(i => i.TestQuestions)
                .ThenInclude(i => i.Question)
                .FirstOrDefaultAsync(i => i.Id == command.Id && 
                                          i.UserId == command.UserId, cancellationToken);

            if (existingTest is null)
                return Error.NotFound(description: "Test not found.");

            if (existingTest.Version != command.Version)
            {
                return ErrorOrExt.ConcurrencyError(existingTest.TestToTestDto());
            }

            Domain.Aggregates.TestAggregate.Test.Update(existingTest, command.Title,
                command.Description, command.Questions?
                    .Select(q => q.QuestionDtoToQuestion())
                    .ToList());

            _dbContext.Set<Domain.Aggregates.TestAggregate.Test>().Update(existingTest);
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