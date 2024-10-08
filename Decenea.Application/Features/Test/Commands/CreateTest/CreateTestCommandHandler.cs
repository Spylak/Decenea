using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Mappers;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Domain.Aggregates.TestAggregate;
using ErrorOr;
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
                    command.Questions?.Select(i => new Domain.Aggregates.QuestionAggregate.Question()
                    {
                        Description = i.Description,
                        Title = i.Title,
                        QuestionType = i.QuestionType,
                        SerializedQuestionContent = i.SerializedQuestionContent,
                        Weight = i.Weight,
                        IsAnswer = true,
                        SecondsToAnswer = i.SecondsToAnswer,
                        Order = i.Order
                    }).ToList());

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