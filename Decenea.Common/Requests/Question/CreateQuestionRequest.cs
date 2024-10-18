using Decenea.Common.DataTransferObjects.Question;

namespace Decenea.Common.Requests.Question;

public class CreateQuestionRequest
{
    public required QuestionDto Question { get; set; }
}