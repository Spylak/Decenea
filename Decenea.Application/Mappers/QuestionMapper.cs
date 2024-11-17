using Decenea.Common.DataTransferObjects.Question;
using Decenea.Domain.Aggregates.QuestionAggregate;
using Decenea.Domain.Helpers;

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
                SerializedQuestionContent = question.SerializedUnAnsweredContent,
                UserId = question.UserId,
                Description = question.Description,
                Title = question.Title,
                Order = null,
                Weight = null,
                Id = question.Id
            };
        }
        else
        {
            questionDto.Version = question.Version;
            questionDto.SerializedQuestionContent = question.SerializedUnAnsweredContent;
            questionDto.UserId = question.UserId;
            questionDto.Description = question.Description;
            questionDto.Title = question.Title;
            questionDto.Order = null;
            questionDto.Weight = null;
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
                Id = questionDto.Id,
                UserId = questionDto.UserId,
                QuestionType = questionDto.QuestionType,
                Title = questionDto.Title,
                Description = questionDto.Description,
                SerializedUnAnsweredContent = QuestionHelper.SetUnAnsweredContent(questionDto.QuestionType, questionDto.SerializedQuestionContent)
            };
        }
        else
        {
            question.UserId = questionDto.UserId;
            question.QuestionType = questionDto.QuestionType;
            question.Title = questionDto.Title;
            question.Description = questionDto.Description;
            question.SerializedUnAnsweredContent = QuestionHelper.SetUnAnsweredContent(question.QuestionType, questionDto.SerializedQuestionContent);
        }
        question.Version = questionDto.Version;


        if (question.Answer is null)
        {
            question.Answer = new QuestionAnswer()
            {
                QuestionId = question.Id,
                SerializedAnsweredContent = questionDto.SerializedQuestionContent
            };
        }
        else
        {
            question.Answer.SerializedAnsweredContent = questionDto.SerializedQuestionContent;
        }
        
        return question;
    }
}