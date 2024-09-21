using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Features.Test.Commands.RemoveTestQuestions;

public class RemoveTestQuestionsCommand : ICommand<ErrorOr<bool>>
{
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
    public required string TestId { get; set; }
    public List<string> QuestionIds { get; set; } = [];
}