namespace AIToolbox.Options.Connectors;

public sealed class MistralAIConnectorOptions
{
    public string Endpoint { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
    public string? ServiceId { get; set; }
}
