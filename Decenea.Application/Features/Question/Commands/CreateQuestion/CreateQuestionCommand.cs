using Decenea.Common.DataTransferObjects.Question;
using FastEndpoints;

namespace Decenea.Application.Features.Question.Commands.CreateQuestion;

public class CreateQuestionCommand : ICommand<ErrorOr<QuestionDto>>
{
    public required string UserId { get; set; }
    public required QuestionDto Question { get; set; }
}