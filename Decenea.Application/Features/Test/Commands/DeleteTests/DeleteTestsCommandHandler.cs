using Decenea.Application.Abstractions.Persistance;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Decenea.Application.Features.Test.Commands.DeleteTests;

public class DeleteTestsCommandHandler : ICommandHandler<DeleteTestsCommand, ErrorOr<bool>>
{
    private readonly IDeceneaDbContext _dbContext;

    public DeleteTestsCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ErrorOr<bool>> ExecuteAsync(DeleteTestsCommand command, CancellationToken ct)
    {
        var test = await _dbContext
            .Set<Domain.Aggregates.TestAggregate.Test>()
            .Where(i => command.TestIds.Contains(i.Id)
                        && i.UserId == command.UserId)
            .ExecuteDeleteAsync(ct);
            
        return test == 1;
    }
}