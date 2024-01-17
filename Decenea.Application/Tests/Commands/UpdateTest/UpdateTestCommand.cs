using Decenea.Application.Abstractions.Messaging;
using Decenea.Common.Common;


namespace Decenea.Application.Tests.Commands.UpdateTest;

public class UpdateTestCommand : UpdateCommand
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public string CityId { get; set; }
}