namespace AIToolbox.Options.Connectors;

public sealed class OllamaConnectorOptions
{
    public string Endpoint { get; set; } = default!;
    public string? ServiceId { get; set; }
}
