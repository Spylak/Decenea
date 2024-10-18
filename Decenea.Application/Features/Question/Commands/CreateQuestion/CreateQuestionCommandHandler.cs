using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Question;
using FastEndpoints;
using Serilog;

namespace Decenea.Application.Features.Question.Commands.CreateQuestion;

public class CreateQuestionCommandHandler : ICommandHandler<CreateQuestionCommand, ErrorOr<QuestionDto>>
{
    private readonly IDeceneaDbContext _dbContext;
    public CreateQuestionCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ErrorOr<QuestionDto>> ExecuteAsync(CreateQuestionCommand command, CancellationToken ct)
    {
        try
        {
            var question = Domain.Aggregates.QuestionAggregate.Question.Create(
                command.Question.Description,
                command.Question.IsAnswer,
                command.Question.Title,
                command.Question.SecondsToAnswer,
                command.Question.Weight,
                command.Question.Order,
                command.UserId,
                command.Question.QuestionType,
                command.Question.SerializedQuestionContent
            );

            _dbContext.ModifiedBy = command.UserId;
            await _dbContext.Set<Domain.Aggregates.QuestionAggregate.Question>()
                .AddAsync(question, ct);

            await _dbContext.SaveChangesAsync(ct);
            return question.QuestionToQuestionDto();
        }
        catch (Exception ex)
        {
            Log.Error("Failed to Create Question from request: {command} with error: {ex}", command, ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}