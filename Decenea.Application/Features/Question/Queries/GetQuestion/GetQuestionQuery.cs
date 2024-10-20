using Decenea.Common.DataTransferObjects.Question;
using FastEndpoints;

namespace Decenea.Application.Features.Question.Queries.GetQuestion;

public class GetQuestionQuery : ICommand<ErrorOr<QuestionDto>>
{
    public required string UserId { get; set; }
    public required string QuestionId { get; set; }
}