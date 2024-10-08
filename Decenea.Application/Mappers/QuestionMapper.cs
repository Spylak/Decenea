using Decenea.Common.DataTransferObjects.Question;
using Decenea.Domain.Aggregates.QuestionAggregate;

namespace Decenea.Application.Mappers;

public static class QuestionMapper
{
    public static QuestionDto QuestionToQuestionDto(this Question question, QuestionDto? questionDto = null)
    {
        if (questionDto is null)
        {
            questionDto = new QuestionDto()
            {
                QuestionType = question.QuestionType,
                Version = question.Version
            };
        }
        
        return questionDto;
    }
    
    public static Question QuestionDtoToQuestion(this QuestionDto questionDto, Question? question = null)
    {
        if (question is null)
        {
            question = new Question()
            {
                QuestionType = questionDto.QuestionType,
                Title = questionDto.Title,
                Description = questionDto.Description,
                SerializedQuestionContent = questionDto.SerializedQuestionContent,
                Version = questionDto.Version ?? string.Empty
            };
        }
        else
        {
            question.QuestionType = questionDto.QuestionType;
            question.Title = questionDto.Title;
            question.Description = questionDto.Description;
            question.SerializedQuestionContent = questionDto.SerializedQuestionContent;
        }
        
        
        return question;
    }
}