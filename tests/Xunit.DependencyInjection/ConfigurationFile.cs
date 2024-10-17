namespace Xunit.DependencyInjection;

public sealed class ConfigurationFile
{
    public required string Path { get; init; }
    public bool Optional { get; init; }
    public bool ReloadOnChange { get; init; }
}
