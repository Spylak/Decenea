using Decenea.Application.Abstractions.Persistance;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.Test.Commands.AddTestQuestions;

public class AddTestQuestionsCommandHandler : ICommandHandler<AddTestQuestionsCommand, ErrorOr<bool>>
{
    private readonly IDeceneaDbContext _dbContext;

    public AddTestQuestionsCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<bool>> ExecuteAsync(AddTestQuestionsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (command.QuestionIds.Count == 0)
                return Error.Failure(description: "No questions to add.");
            
            var test = await _dbContext
                .Set<Domain.Aggregates.TestAggregate.Test>()
                .FirstOrDefaultAsync(i => i.Id == command.TestId && 
                                          i.UserId == command.UserId, cancellationToken);
            
            if(test is null)
                return Error.NotFound(description: "Test not found.");
            
            _dbContext.ModifiedBy = command.UserId;
            
            foreach (var questionId in command.QuestionIds)
            {
                test.AddQuestion(questionId);
            }
            
            _dbContext.Set<Domain.Aggregates.TestAggregate.Test>().Update(test);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            Log.Error("Failed to AddQuestions from request: {command} with error: {ex}", command, ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}