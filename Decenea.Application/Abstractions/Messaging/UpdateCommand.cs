namespace Decenea.Application.Abstractions.Messaging;

public abstract class UpdateCommand
{
    public required string Version { get; init; }
}