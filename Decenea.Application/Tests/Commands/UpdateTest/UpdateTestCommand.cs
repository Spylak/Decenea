using Decenea.Application.Abstractions.Messaging;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using FastEndpoints;


namespace Decenea.Application.Tests.Commands.UpdateTest;

public class UpdateTestCommand : UpdateCommand , ICommand<Result<TestDto, Exception>>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
}