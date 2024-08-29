using Decenea.Domain.Helpers;

namespace Decenea.Domain.Common;

public abstract class Versioned
{
    public string Version { get; set; } = RandomStringGenerator.RandomString(8);
}