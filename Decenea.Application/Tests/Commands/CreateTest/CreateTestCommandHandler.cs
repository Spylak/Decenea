using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Domain.Aggregates.TestAggregate;
using ErrorOr;
using FastEndpoints;
using Serilog;

namespace Decenea.Application.Tests.Commands.CreateTest;

public class CreateTestCommandHandler : ICommandHandler<CreateTestCommand, ErrorOr<TestDto>>
{    
    private readonly IDeceneaDbContext _dbContext;

    public CreateTestCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<TestDto>> ExecuteAsync(CreateTestCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var createResult = Test.Create(command.Title,
                command.Description,
                command.ContactEmail,
                command.ContactPhone,
                command.UserId);
            
            _dbContext.ModifiedBy = command.UserId;
            await _dbContext.Set<Test>().AddAsync(createResult, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return createResult.TestToTestDto();
        }
        catch (Exception ex)
        {
            Log.Error("Failed to CreateTest from request: {command} with error: {ex}", command,ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}