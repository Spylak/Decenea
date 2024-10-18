using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Test;
using FastEndpoints;
using Serilog;

namespace Decenea.Application.Features.Test.Commands.CreateTest;

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
            var createResult = Domain
                .Aggregates
                .TestAggregate
                .Test
                .Create(command.Title,
                    command.Description,
                    command.UserId,
                    command.MinutesToComplete,
                    command.Questions?.Select(i => Domain.Aggregates.QuestionAggregate.Question.Create(
                        i.Description,
                        i.IsAnswer,
                        i.Title,
                        i.SecondsToAnswer,
                        i.Weight,
                        i.Order,
                        command.UserId,
                        i.QuestionType,
                        i.SerializedQuestionContent
                    )).ToList());

            _dbContext.ModifiedBy = command.UserId;
            await _dbContext.Set<Domain.Aggregates.TestAggregate.Test>()
                .AddAsync(createResult, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return createResult.TestToTestDto();
        }
        catch (Exception ex)
        {
            Log.Error("Failed to CreateTest from request: {command} with error: {ex}", command, ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}