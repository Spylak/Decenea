using Decenea.Common.DataTransferObjects.Answer;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Domain.Aggregates.TestAggregate;

namespace Decenea.Application.Mappers;

public static class TestMapper
{
    public static TestDto TestToTestDto(this Test test,
        TestDto? testDto = null,
        bool includeQuestions = false,
        bool includeTestUsers = false)
    {
        if (testDto != null)
        {
            testDto.Title = test.Title;
            testDto.Description = test.Description;
            testDto.MinutesToComplete = test.MinutesToComplete;
        }
        else
        {
            testDto = new TestDto
            {
                Version = test.Version,
                Title = test.Title,
                Description = test.Description,
                MinutesToComplete = test.MinutesToComplete
            };
        }
        testDto.Id = test.Id;
        testDto.Questions = includeQuestions ? test.TestQuestions
            .Where(i => i.Question != null)
            .Select(i => i.Question!.QuestionToQuestionDto())
            .ToList() : [];

        testDto.TestUsers = includeTestUsers
            ? test.TestUsers.Select(i => new TestUserDto
            {
                UserEmail = i.User?.Email ?? "Not Found",
                UserId = i.UserId,
            }).ToList()
            : [];
        
        return testDto;
    }
    public static ActiveTestDto TestToActiveTestDto(this Test test,
        ActiveTestDto? activeTestDto = null)
    {
        if (activeTestDto != null)
        {
            activeTestDto.Title = test.Title;
            activeTestDto.Description = test.Description;
            activeTestDto.MinutesToComplete = test.MinutesToComplete;
        }
        else
        {
            activeTestDto = new ActiveTestDto()
            {
                Version = test.Version,
                Title = test.Title,
                Description = test.Description,
                MinutesToComplete = test.MinutesToComplete
            };
        }
        activeTestDto.Id = test.Id;
        activeTestDto.Questions = test.TestQuestions
            .Where(i => i.Question != null)
            .Select(i => i.Question!.QuestionToQuestionDto())
            .ToList();

        activeTestDto.TestAnswers = test
            .TestUsers
            .SelectMany(i => i.TestAnswers)
            .Select(i => new TestAnswerDto
            {
                QuestionId = i.QuestionId,
                Id = i.Id,
                SerializedQuestionContent = i.SerializedQuestionContent
            })
            .ToList();
        
        return activeTestDto;
    }
}