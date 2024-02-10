
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
    public List<int>? TestIds { get; set; }
}

public class QuestionBaseModel<T> : QuestionBaseModel where T : class
{
    public QuestionBaseModel(T questionContent) : base(typeof(T).Name)
    {
        QuestionContent = questionContent;
    }
    public T QuestionContent { get; set; }
}