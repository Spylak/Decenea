using Decenea.Common.DataTransferObjects.Question;
using Decenea.Common.DataTransferObjects.Test;
using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Features.Test.Commands.CreateTest;

public class CreateTestCommand : ICommand<ErrorOr<TestDto>>
{
    public string? Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string UserId { get; set; }
    public List<QuestionDto>? Questions { get; set; }
}