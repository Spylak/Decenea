using Decenea.Application.Abstractions.Messaging;
using Decenea.Common.DataTransferObjects.Question;
using Decenea.Common.DataTransferObjects.Test;
using ErrorOr;
using FastEndpoints;

namespace Decenea.Application.Features.Test.Commands.UpdateTest;

public class UpdateTestCommand : UpdateCommand , ICommand<ErrorOr<TestDto>>
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
    public List<QuestionDto>? Questions { get; set; }
}