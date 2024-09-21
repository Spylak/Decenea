using Decenea.Common.DataTransferObjects.Question;
using ErrorOr;
using Decenea.Common.DataTransferObjects.Test;
using FastEndpoints;

namespace Decenea.Application.Tests.Commands.CreateTest;

public class CreateTestCommand : ICommand<ErrorOr<TestDto>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public required string UserId { get; set; }
    public List<QuestionDto> Questions { get; set; } = [];
}