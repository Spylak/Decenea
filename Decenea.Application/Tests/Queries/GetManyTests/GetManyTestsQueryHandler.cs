using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Domain.Aggregates.TestAggregate;
using ErrorOr;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;


namespace Decenea.Application.Tests.Queries.GetManyTests;

public class GetManyTestsQueryHandler : ICommandHandler<GetManyTestsQuery, ErrorOr<IEnumerable<TestDto>>>
{
    private readonly IDeceneaDbContext _dbContext;

    public GetManyTestsQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<IEnumerable<TestDto>>> ExecuteAsync(GetManyTestsQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var dbQuery = _dbContext.Set<Test>().AsQueryable();
                
            if (query.Skip.HasValue)
            {
                dbQuery = dbQuery.Skip(query.Skip.Value);
            }
                
            if (query.Take.HasValue)
            {
                dbQuery = dbQuery.Take(query.Take.Value);
            }

            var testDtos = await dbQuery
                .Select(i => i.TestToTestDto(null))
                .ToListAsync(cancellationToken);
            
            return testDtos;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: "Didn't manage to get list of tests.");
        }
    }
}