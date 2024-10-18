using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Test;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Application.Features.Test.Queries.GetTest;

public class GetTestQueryHandler : ICommandHandler<GetTestQuery, ErrorOr<TestDto>>
{
    private readonly IDeceneaDbContext _dbContext;
    public GetTestQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ErrorOr<TestDto>> ExecuteAsync(GetTestQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var test = await _dbContext.Set<Domain.Aggregates.TestAggregate.Test>()
                .Include(i => i.TestQuestions)
                .ThenInclude(i => i.Question)
                .FirstOrDefaultAsync(i => i.Id.Equals(query.Id), cancellationToken);
            
            if(test is null)
                return Error.NotFound(description: "Test not found.");

            return test.TestToTestDto();
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: "Didn't manage to get Micro Ad.");
        }
    }
}