using Decenea.Application.Abstractions.Messaging;
using ErrorOr;
using Decenea.Common.DataTransferObjects.Test;
using FastEndpoints;


namespace Decenea.Application.Tests.Commands.UpdateTest;

public class UpdateTestCommand : UpdateCommand , ICommand<ErrorOr<TestDto>>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
}