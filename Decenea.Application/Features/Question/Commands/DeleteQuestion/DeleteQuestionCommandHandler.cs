using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Features.Question.Queries.GetQuestion;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Question;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.Question.Commands.DeleteQuestion;

public class DeleteQuestionCommandHandler : ICommandHandler<DeleteQuestionsCommand, ErrorOr<bool>>
{
    private readonly IDeceneaDbContext _dbContext;
    public DeleteQuestionCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ErrorOr<bool>> ExecuteAsync(DeleteQuestionsCommand command, CancellationToken ct)
    {
        try
        {
            var result = await _dbContext
                .Set<Domain.Aggregates.QuestionAggregate.Question>()
                .Where(i => i.UserId == command.UserId && command.QuestionIds.Contains(i.Id))
                .ExecuteDeleteAsync(ct);

            return result == 1;
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return Error.Unexpected(description: "Didn't manage to get list of tests.");
        }
    }
}