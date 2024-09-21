using Decenea.Application.Abstractions.Persistance;
using Decenea.Domain.Aggregates.TestAggregate;
using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Tests.Commands.RemoveTestQuestions;

public class RemoveTestQuestionsCommandHandler : ICommandHandler<RemoveTestQuestionsCommand, ErrorOr<bool>>
{
    private readonly IDeceneaDbContext _dbContext;

    public RemoveTestQuestionsCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<bool>> ExecuteAsync(RemoveTestQuestionsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (command.QuestionIds.Count == 0)
                return Error.Failure(description: "No questions to add.");
            
            var test = await _dbContext
                .Set<Test>()
                .FirstOrDefaultAsync(i => i.Id == command.TestId && 
                                          i.UserId == command.UserId, cancellationToken);
            
            if(test is null)
                return Error.NotFound(description: "Test not found.");
            
            _dbContext.ModifiedBy = command.UserId;
            
            foreach (var questionId in command.QuestionIds)
            {
                test.RemoveQuestion(questionId);
            }
            
            _dbContext.Set<Test>().Update(test);
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