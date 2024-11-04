using Decenea.Application.Abstractions.Persistance;
using Decenea.Common.DataTransferObjects.Answer;
using Decenea.Domain.Aggregates.TestAggregate;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Decenea.Application.Features.Test.Commands.UpsertAnswers;

public class UpsertAnswersCommandHandler : ICommandHandler<UpsertAnswersCommand, ErrorOr<List<TestAnswerDto>>>
{
    private readonly IDeceneaDbContext _dbContext;

    public UpsertAnswersCommandHandler(IDeceneaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<List<TestAnswerDto>>> ExecuteAsync(UpsertAnswersCommand command, CancellationToken ct)
    {
        try
        {
            _dbContext.ModifiedBy = command.UserId;
            var testUser = await _dbContext.Set<TestUser>()
                .Include(i => i.TestAnswers)
                .Include(i => i.Test)
                .ThenInclude(i => i!.TestQuestions)
                .AsSplitQuery()
                .FirstOrDefaultAsync(i => i.EndTime >= DateTime.UtcNow && i.UserId == command.UserId && i.TestId == command.TestId, ct);

            if (testUser == null)
            {
                var testGroup = await _dbContext.Set<TestGroup>()
                    .FirstOrDefaultAsync(i => i.TestId.Equals(command.TestId) 
                                              && i.Group!.GroupMembers.Select(j => j.GroupUserEmail).Contains(command.UserEmail), ct);
                
                if (testGroup is not null)
                {
                    testUser = new TestUser()
                    {
                        UserId = command.UserId,
                        TestId = command.TestId,
                    };
                }
            }
            
            if (testUser == null)
                return Error.NotFound(description:"Test user not found");

            var testQuestionIds = testUser
                .Test!
                .TestQuestions
                .Where(i => command.Answers.Select(j => j.QuestionId).Contains(i.QuestionId))
                .Select(i => i.QuestionId)
                .ToList();
            
            var addAnswers = command
                .Answers
                .Where(i => i.Version is null)
                .Where(i => testQuestionIds.Contains(i.QuestionId))
                .Select(a => new TestAnswer()
                {
                    QuestionId = a.QuestionId,
                    TestUserId = testUser.Id,
                    SerializedQuestionContent = a.SerializedQuestionContent,
                })
                .ToList();

            var updateAnswers = command
                .Answers
                .Where(i => i.Version is not null)
                .Where(i => testQuestionIds.Contains(i.QuestionId))
                .ToList();
            
            var existingAnswers = new List<TestAnswer>();
            if (updateAnswers.Count > 0)
            {
                existingAnswers = testUser.TestAnswers
                    .Where(i => updateAnswers.Any(a => a.QuestionId == i.QuestionId))
                    .ToList();
            }

            foreach (var answer in updateAnswers)
            {
                answer.SerializedQuestionContent = command
                    .Answers
                    .First(i => i.Id == answer.Id)
                    .SerializedQuestionContent;
                _dbContext.Set<TestUser>().Update(testUser);
            }

            await _dbContext.SaveChangesAsync(ct);

            var result = addAnswers.Union(existingAnswers)
                .Select(i => new TestAnswerDto
                {
                    Id = i.QuestionId,
                    QuestionId = i.QuestionId,
                    SerializedQuestionContent = i.SerializedQuestionContent,
                    Version = i.Version
                }).ToList();
            
            return result;
        }
        catch (Exception ex)
        {
            Log.Error("Failed to UpsertTestAnswers Answers from request: {command} with error: {ex}", command, ex);
            return Error.Unexpected(description: ex.Message);
        }
    }
}