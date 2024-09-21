using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Tests.Commands.AddTestQuestions;

public class AddTestQuestionsCommand : ICommand<ErrorOr<bool>>
{
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
    public required string TestId { get; set; }
    public List<string> QuestionIds { get; set; } = [];
}