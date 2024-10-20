using Decenea.Common.DataTransferObjects.Question;
using FastEndpoints;

namespace Decenea.Application.Features.Question.Commands.CreateQuestion;

public class CreateQuestionsCommand : ICommand<ErrorOr<List<QuestionDto>>>
{
    public string? TestId { get; set; }
    public required string UserId { get; set; }
    public required List<QuestionDto> Questions { get; set; }
}