using Decenea.Common.DataTransferObjects.Question;
using FastEndpoints;

namespace Decenea.Application.Features.Question.Queries.GetManyQuestions;

public class GetManyQuestionsQuery : ICommand<ErrorOr<IEnumerable<QuestionDto>>>
{
    public required string UserId { get; set; }
}