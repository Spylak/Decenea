using Decenea.Common.DataTransferObjects.Question;
using Decenea.Common.DataTransferObjects.Test;
using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Features.Test.Commands.CreateTest;

public class CreateTestCommand : ICommand<ErrorOr<TestDto>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public required string UserId { get; set; }
    public List<string> QuestionIds { get; set; } = [];
}