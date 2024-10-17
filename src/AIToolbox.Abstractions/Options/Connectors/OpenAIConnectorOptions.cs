namespace AIToolbox.Options.Connectors;

public sealed class OpenAIConnectorOptions
{
    public string ApiKey { get; set; } = default!;
    public string? OrgId { get; set; }
    public string? ServiceId { get; set; }
}
