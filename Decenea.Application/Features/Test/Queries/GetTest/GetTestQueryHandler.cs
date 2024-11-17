using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Domain.Aggregates.TestAggregate;
using Decenea.Domain.Extensions;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
            var testQuery = _dbContext.Set<Domain.Aggregates.TestAggregate.Test>()
                .AsQueryable();
            
            if (query.IncludeQuestions)
            {
                testQuery = testQuery.Include(i => i.TestQuestions)
                    .ThenInclude(i => i.Question)
                    .AsSplitQuery();
            }
            
            if (query.IncludeGroups)
            {
                testQuery = testQuery.Include(i => i.TestGroups)
                    .ThenInclude(i => i.Group)
                    .AsSplitQuery();
            }
            
            if (query.IncludeUsers)
            {
                testQuery = testQuery.Include(i => i.TestUsers)
                    .ThenInclude(i => i.User)
                    .AsSplitQuery();
            }
            
            var test = await testQuery
                .FirstOrDefaultAsync(i => i.Id.Equals(query.Id) &&
                                          (i.UserId == query.UserId
                                           || i.TestUsers.Select(j => j.UserId).Contains(query.UserId)),
                    cancellationToken);
            
            if (test is null)
            {
                var testGroup = await _dbContext.Set<TestGroup>()
                    .Include(i => i.Test)
                    .ThenInclude(i => i!.TestQuestions)
                    .ThenInclude(i => i.Question)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(i => i.TestId.Equals(query.Id) && i.EndTime >= DateTime.UtcNow &&
                        i.Group!.GroupMembers.Select(j => j.GroupUserEmail).Contains(query.UserEmail), cancellationToken);
                
                test = testGroup?.Test;
            }

            if (test is null)
            {
                var testUser = await _dbContext.Set<TestUser>()
                    .Include(i => i.Test)
                    .ThenInclude(i => i!.TestQuestions)
                    .ThenInclude(i => i.Question)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(i => i.EndTime >= DateTime.UtcNow && i.UserId == query.UserId && i.TestId == query.Id, cancellationToken);
                
                test = testUser?.Test;
            }
            
            if(test is null)
                return Error.NotFound(description: "Test not found.");
            
            return test.TestToTestDto(null, query.IncludeQuestions, query.IncludeUsers, query.IncludeGroups);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error occured while executing GetTestQuery");
            return Error.Unexpected(description: "Didn't manage to get Test.");
        }
    }
}