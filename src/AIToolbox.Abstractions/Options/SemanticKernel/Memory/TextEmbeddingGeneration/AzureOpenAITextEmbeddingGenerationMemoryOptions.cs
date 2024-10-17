namespace AIToolbox.Options.SemanticKernel;

public sealed class AzureOpenAITextEmbeddingGenerationMemoryOptions
{
    public string DeploymentName { get; set; } = default!;
    public string? Endpoint { get; set; }
    public string? ApiKey { get; set; }
    public string? ModelId { get; set; }
    public int? Dimensions { get; set; }
}
