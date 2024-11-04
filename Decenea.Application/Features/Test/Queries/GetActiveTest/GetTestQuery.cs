using Decenea.Common.DataTransferObjects.Test;
using FastEndpoints;

namespace Decenea.Application.Features.Test.Queries.GetActiveTest;

public class GetActiveTestQuery : ICommand<ErrorOr<TestDto>>
{
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public required string UserEmail { get; set; }
    public bool IncludeQuestions { get; set; }
}