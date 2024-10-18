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
                Version = question.Version,
                IsAnswer = question.IsAnswer,
                SerializedQuestionContent = question.SerializedQuestionContent,
                UserId = question.UserId,
                Description = question.Description,
                Title = question.Title,
                Order = question.Order,
                Weight = question.Weight,
                Id = question.Id
            };
        }
        else
        {
            questionDto.Version = question.Version;
            questionDto.IsAnswer = question.IsAnswer;
            questionDto.SerializedQuestionContent = question.SerializedQuestionContent;
            questionDto.UserId = question.UserId;
            questionDto.Description = question.Description;
            questionDto.Title = question.Title;
            questionDto.Order = question.Order;
            questionDto.Weight = question.Weight;
            questionDto.Id = question.Id;
        }
        
        return questionDto;
    }
    
    public static Question QuestionDtoToQuestion(this QuestionDto questionDto, Question? question = null)
    {
        if (question is null)
        {
            question = new Question()
            {
                UserId = questionDto.UserId,
                QuestionType = questionDto.QuestionType,
                Title = questionDto.Title,
                Description = questionDto.Description,
                SerializedQuestionContent = questionDto.SerializedQuestionContent,
                Version = questionDto.Version
            };
        }
        else
        {
            question.Version = questionDto.Version;
            question.UserId = questionDto.UserId;
            question.QuestionType = questionDto.QuestionType;
            question.Title = questionDto.Title;
            question.Description = questionDto.Description;
            question.SerializedQuestionContent = questionDto.SerializedQuestionContent;
        }
        
        
        return question;
    }
}