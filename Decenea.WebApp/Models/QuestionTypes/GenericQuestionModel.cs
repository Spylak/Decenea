using System.Text.Json;
using Decenea.Common.Enums;

namespace Decenea.WebApp.Models.QuestionTypes;

public class GenericQuestionModel
{ 
    public GenericQuestionModel(QuestionType? questionType)
    {
        if (questionType is null)
        {
            throw new Exception("QuestionType is null");
        }
        
        QuestionType = questionType.Value;
    }
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string Description { get; set; } = "";
    public string Title { get; set; } = "";
    public int SecondsToAnswer { get; set; } 
    public int Order { get; set; } 
    public double Weight { get; set; }
    public QuestionType QuestionType { get; init; }
    public string SerializedQuestionContent { get; set; } = string.Empty;
    public static GenericQuestionModel<T> ConvertToGenericModel<T>(GenericQuestionModel nonGenericModel) where T : QuestionBase
    {
        T questionContent = JsonSerializer.Deserialize<T>(nonGenericModel.SerializedQuestionContent)!;

        if (questionContent == null)
        {
            throw new InvalidOperationException("Deserialization of question content failed.");
        }

        var genericModel = new GenericQuestionModel<T>(questionContent)
        {
            Id = nonGenericModel.Id,
            Description = nonGenericModel.Description,
            Title = nonGenericModel.Title,
            SecondsToAnswer = nonGenericModel.SecondsToAnswer,
            Order = nonGenericModel.Order,
            Weight = nonGenericModel.Weight,
        };

        return genericModel;
    }
    
    public static GenericQuestionModel ConvertToNonGenericModel<T>(GenericQuestionModel<T> genericModel) where T : QuestionBase
    {
        var questionType = (QuestionType?)typeof(T)
            .GetProperty(nameof(Common.Enums.QuestionType))?
            .GetValue(genericModel.QuestionContent);
        
        if (!questionType.HasValue)
            throw new Exception("Question type is empty.");
            
        var nonGenericModel = new GenericQuestionModel(questionType.Value)
        {
            Id = genericModel.Id,
            Description = genericModel.Description,
            Title = genericModel.Title,
            SecondsToAnswer = genericModel.SecondsToAnswer,
            Order = genericModel.Order,
            Weight = genericModel.Weight,
            SerializedQuestionContent = JsonSerializer.Serialize(genericModel.QuestionContent)
        };

        return nonGenericModel;
    }
}

public class GenericQuestionModel<T> : GenericQuestionModel where T : QuestionBase
{
    private T _questionContent;
    public GenericQuestionModel(T questionContent) : base((QuestionType?)typeof(T).GetProperty(nameof(Common.Enums.QuestionType))?.GetValue(questionContent))
    {
        SerializedQuestionContent = JsonSerializer.Serialize(questionContent);
        _questionContent = questionContent;
    }

    public T QuestionContent
    {
        get => _questionContent;
        set
        {
            SerializedQuestionContent = JsonSerializer.Serialize(value);
            _questionContent = value;
        }
    }
}