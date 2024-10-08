using Decenea.Common.DataTransferObjects.Test;
using Decenea.Domain.Aggregates.TestAggregate;

namespace Decenea.Application.Mappers;

public static class TestMapper
{
    public static TestDto TestToTestDto(this Test test, TestDto? testDto = null)
    {
        testDto ??= new TestDto {Version = test.Version};
        testDto.Title = test.Title;
        testDto.Description = test.Description;
        testDto.Questions = test.TestQuestions
            .Where(i => i.Question != null)
            .Select(i => i.Question!.QuestionToQuestionDto())
            .ToList();
        
        return testDto;
    }
}