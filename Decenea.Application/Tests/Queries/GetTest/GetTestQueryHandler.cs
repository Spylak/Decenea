using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Domain.Aggregates.TestAggregate;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;


namespace Decenea.Application.Tests.Queries.GetTest;

public class GetTestQueryHandler : ICommandHandler<GetTestQuery, Result<TestDto, Exception>>
{
    private readonly IDeceneaDbContext _dbContext;
    public GetTestQueryHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<TestDto, Exception>> ExecuteAsync(GetTestQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var testDto = await _dbContext.Set<Test>()
                .FirstOrDefaultAsync(i => i.Id.Equals(query.Id), cancellationToken);

            return Result<TestDto, Exception>.Anticipated(testDto?.TestToTestDto());
        }
        catch (Exception ex)
        {
            return Result<TestDto, Exception>
                .Excepted(ex, ["Didn't manage to get Micro Ad."]);
        }
    }
}