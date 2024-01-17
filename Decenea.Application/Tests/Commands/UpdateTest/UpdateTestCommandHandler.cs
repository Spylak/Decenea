using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.Common;
using Decenea.Domain.Aggregates.TestAggregate;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Tests.Commands.UpdateTest;

public class UpdateTestCommandHandler
{
    private readonly IDeceneaDbContext _dbContext;

    public UpdateTestCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async ValueTask<Result<object, Exception>> Handle(UpdateTestCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var existingTest = await _dbContext
                .Set<Test>()
                .FirstOrDefaultAsync(i => i.Id == command.Id, cancellationToken);

            if (existingTest is null)
                return Result<object, Exception>.Anticipated(null, "Test not found.");

            if (existingTest.Version != command.Version)
                return Result<object, Exception>.Anticipated(existingTest, "Concurrency issue.", false);

            var updateResult = Test.Update(existingTest, command.Title,
                command.Description,
                command.CityId,
                command.ContactEmail,
                command.ContactPhone);

            if (!updateResult.IsSuccess)
                return Result<object, Exception>.Anticipated(null, updateResult.Messages);

            _dbContext.Set<Test>().Update(existingTest);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            if(!result.IsSuccess)
                return Result<object, Exception>.Anticipated(null, result.Messages);
                
            return Result<object, Exception>.Anticipated(null, "Successfully updated entity.", true);
        }
        catch (Exception ex)
        {
            Log.Error("Failed to UpdateTest from request: {command} with error: {ex}", command, ex);
            return Result<object, Exception>.Excepted(ex);
        }
    }
}