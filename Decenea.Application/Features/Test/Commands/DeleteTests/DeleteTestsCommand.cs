using FastEndpoints;
using ErrorOr;

namespace Decenea.Application.Features.Test.Commands.DeleteTests;

public class DeleteTestsCommand : ICommand<ErrorOr<bool>>
{
    public required List<string> TestIds { get; set; }
    public required string UserId { get; set; }
}