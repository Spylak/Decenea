using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Question;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.Question.Queries.GetQuestion;

public class GetQuestionQueryHandler : ICommandHandler<GetQuestionQuery, ErrorOr<QuestionDto>>
{
    private readonly IDeceneaDbContext _dbContext;
    public GetQuestionQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ErrorOr<QuestionDto>> ExecuteAsync(GetQuestionQuery query, CancellationToken ct)
    {
        try
        {
            var questionDto = await _dbContext
                .Set<Domain.Aggregates.QuestionAggregate.Question>()
                .FirstOrDefaultAsync(i => i.UserId == query.UserId && i.Id == query.QuestionId, ct);
            
            if (questionDto == null)
            {
                return Error.NotFound(description:"Question not found");
            }
            
            return questionDto.QuestionToQuestionDto();
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return Error.Unexpected(description: "Didn't manage to get list of tests.");
        }
    }
}