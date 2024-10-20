using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Question;
using FastEndpoints;
using Serilog;

namespace Decenea.Application.Features.Question.Commands.CreateQuestion;

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
            var questions = new List<Domain.Aggregates.QuestionAggregate.Question>();

            foreach (var question in command.Questions)
            {
                questions.Add(Domain.Aggregates.QuestionAggregate.Question.Create(
                    question.Description,
                    question.IsAnswer,
                    question.Title,
                    question.SecondsToAnswer,
                    question.Weight,
                    question.Order,
                    command.UserId,
                    question.QuestionType,
                    question.SerializedQuestionContent
                ));
            }

            _dbContext.ModifiedBy = command.UserId;
            await _dbContext
                .Set<Domain.Aggregates.QuestionAggregate.Question>()
                .AddRangeAsync(questions, ct);

            await _dbContext.SaveChangesAsync(ct);
            return questions.Select(i => i.QuestionToQuestionDto()).ToList();
        }
        catch (Exception ex)
        {
            Log.Error("Failed to Create Questions from request: {command} with error: {ex}", command, ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}