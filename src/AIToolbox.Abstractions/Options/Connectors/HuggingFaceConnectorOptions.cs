namespace AIToolbox.Options.Connectors;

public sealed class HuggingFaceConnectorOptions
{
    public string Endpoint { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
    public string? ServiceId { get; set; }
}
