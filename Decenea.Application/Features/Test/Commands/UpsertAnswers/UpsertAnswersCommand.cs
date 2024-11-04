using Decenea.Common.DataTransferObjects.Answer;
using FastEndpoints;

namespace Decenea.Application.Features.Test.Commands.UpsertAnswers;

public class UpsertAnswersCommand : ICommand<ErrorOr<List<TestAnswerDto>>>
{
    public required string TestId { get; set; }
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
    public required List<TestAnswerDto> Answers { get; set; }
}