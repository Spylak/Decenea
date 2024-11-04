using System.Text.Json;
using Decenea.Common.DataTransferObjects.Answer;
using Decenea.Common.DataTransferObjects.Question;
using Decenea.Common.DataTransferObjects.Question.QuestionTypes;

namespace Decenea.WebApp.Mappers;

public static class QuestionModelMapper
{
    // Map from GenericQuestionModel to QuestionDto
    public static QuestionDto ToDto(this GenericQuestionModel model)
    {
        return new QuestionDto
        {
            Id = model.Id,
            Description = model.Description,
            Title = model.Title,
            SecondsToAnswer = model.SecondsToAnswer,
            Order = model.Order,
            Weight = model.Weight,
            QuestionType = model.QuestionType,
            SerializedQuestionContent = model.SerializedQuestionContent,
            Version = model.Version
        };
    }
    
    public static TestAnswerDto ToAnswerDto(this GenericQuestionModel model)
    {
        return new TestAnswerDto
        {
            QuestionId = model.Id,
            SerializedQuestionContent = model.SerializedQuestionContent
        };
    }

    // Map from GenericQuestionModel<T> to QuestionDto
    public static QuestionDto ToDto<T>(this GenericQuestionModel<T> genericModel) where T : QuestionBase
    {
        return new QuestionDto
        {
            Id = genericModel.Id,
            Description = genericModel.Description,
            Title = genericModel.Title,
            SecondsToAnswer = genericModel.SecondsToAnswer,
            Order = genericModel.Order,
            Weight = genericModel.Weight,
            QuestionType = genericModel.QuestionType,
            SerializedQuestionContent = genericModel.SerializedQuestionContent,
            Version = genericModel.Version
        };
    }

    // Map from QuestionDto to GenericQuestionModel
    public static GenericQuestionModel ToGenericQuestionModel(this QuestionDto dto)
    {
        return new GenericQuestionModel
        {
            Id = dto.Id ?? Ulid.NewUlid().ToString(),
            Description = dto.Description,
            Title = dto.Title,
            SecondsToAnswer = dto.SecondsToAnswer,
            Order = dto.Order,
            Weight = dto.Weight,
            QuestionType = dto.QuestionType,
            SerializedQuestionContent = dto.SerializedQuestionContent
        };
    }

    // Map from QuestionDto to GenericQuestionModel<T>
    public static GenericQuestionModel<T> ToGenericQuestionModel<T>(this QuestionDto dto) where T : QuestionBase
    {
        T questionContent = JsonSerializer.Deserialize<T>(dto.SerializedQuestionContent)!;

        if (questionContent == null)
        {
            throw new InvalidOperationException("Deserialization of question content failed.");
        }

        return new GenericQuestionModel<T>(questionContent)
        {
            Id = dto.Id ?? Ulid.NewUlid().ToString(),
            Description = dto.Description,
            Title = dto.Title,
            SecondsToAnswer = dto.SecondsToAnswer,
            Order = dto.Order,
            Weight = dto.Weight,
            QuestionType = dto.QuestionType
        };
    }
}
