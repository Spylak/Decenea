using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Question;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.Question.Queries.GetManyQuestions;

public class GetManyQuestionsQueryHandler : ICommandHandler<GetManyQuestionsQuery, ErrorOr<IEnumerable<QuestionDto>>>
{
    private readonly IDeceneaDbContext _dbContext;
    public GetManyQuestionsQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ErrorOr<IEnumerable<QuestionDto>>> ExecuteAsync(GetManyQuestionsQuery query, CancellationToken ct)
    {
        try
        {
            var questionDtos = await _dbContext
                .Set<Domain.Aggregates.QuestionAggregate.Question>()
                .Where(i => i.UserId == query.UserId)
                .Select(i => i.QuestionToQuestionDto(null))
                .ToListAsync(ct);
            
            return questionDtos;
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return Error.Unexpected(description: "Didn't manage to get list of tests.");
        }
    }
}