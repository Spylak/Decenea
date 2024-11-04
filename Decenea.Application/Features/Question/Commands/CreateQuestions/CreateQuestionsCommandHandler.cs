using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Question;
using FastEndpoints;
using Serilog;

namespace Decenea.Application.Features.Question.Commands.CreateQuestions;

public class CreateQuestionsCommandHandler : ICommandHandler<CreateQuestionsCommand, ErrorOr<List<QuestionDto>>>
{
    private readonly IDeceneaDbContext _dbContext;

    public CreateQuestionsCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<List<QuestionDto>>> ExecuteAsync(CreateQuestionsCommand command, CancellationToken ct)
    {
        try
        {
            var questions = command
                .Questions
                .Select(question => Domain.Aggregates.QuestionAggregate.Question.Create(
                    question.Description,
                    question.Title,
                    command.UserId,
                    question.QuestionType, 
                    question.SerializedQuestionContent, 
                    command.TestId))
                .ToList();

            _dbContext.ModifiedBy = command.UserId;
            await _dbContext
                .Set<Domain.Aggregates.QuestionAggregate.Question>()
                .AddRangeAsync(questions, ct);
            
            await _dbContext.SaveChangesAsync(ct);
            return questions.Select(i => i.QuestionToQuestionDto()).ToList();
        }
        catch (Exception ex)
        {
            Log.Error("Failed to UpsertTestAnswers Questions from request: {command} with error: {ex}", command, ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}