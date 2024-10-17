namespace AIToolbox.Options.Connectors;

public sealed class GoogleConnectorOptions
{
    public string ApiKey { get; set; } = default!;
    public GoogleAIVersion ApiVersion { get; set; } = GoogleAIVersion.V1Beta;
    public string? ServiceId { get; set; }
}
