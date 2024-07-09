
using System.Text.Json;

namespace Decenea.WebApp.Models.QuestionTypes;

public class QuestionBaseModel
{
    public QuestionBaseModel(string questionType)
    {
        QuestionType = questionType;
    }
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string Description { get; set; } = "";
    public string Title { get; set; } = "";
    public int SecondsToAnswer { get; set; } 
    public int Order { get; set; } 
    public double Weight { get; set; }
    public string QuestionType { get; init; }
    public string SerializedQuestionContent { get; set; } = string.Empty;
    public static QuestionBaseModel<T> ConvertToGenericBaseModel<T>(QuestionBaseModel nonGenericModel) where T : class
    {
        T questionContent = JsonSerializer.Deserialize<T>(nonGenericModel.SerializedQuestionContent)!;

        if (questionContent == null)
        {
            throw new InvalidOperationException("Deserialization of question content failed.");
        }

        var genericModel = new QuestionBaseModel<T>(questionContent)
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
    
    public static QuestionBaseModel ConvertToNonGenericBaseModel<T>(QuestionBaseModel<T>? genericModel) where T : class
    {
        if (genericModel is null)
            return new QuestionBaseModel(typeof(T).Name);
        
        var nonGenericModel = new QuestionBaseModel(genericModel.QuestionType)
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

public class QuestionBaseModel<T> : QuestionBaseModel where T : class
{
    private T _questionContent;
    public QuestionBaseModel(T questionContent) : base(typeof(T).Name)
    {
        SerializedQuestionContent = JsonSerializer.Serialize(questionContent);
        QuestionContent = questionContent;
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