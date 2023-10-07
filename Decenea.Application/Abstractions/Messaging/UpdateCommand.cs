namespace Decenea.Application.Abstractions.Messaging;

public class UpdateCommand
{
    public required string Version { get; init; }
}