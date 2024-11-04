using FastEndpoints;

namespace Decenea.Application.Features.Question.Commands.DeleteQuestions;

public class DeleteQuestionsCommand : ICommand<ErrorOr<bool>>
{
    public required string UserId { get; set; }
    public required List<string> QuestionIds { get; set; }
}