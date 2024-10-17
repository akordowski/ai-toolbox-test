namespace AIToolbox.Options.SemanticKernel;

public sealed class OpenAITextEmbeddingGenerationMemoryOptions
{
    public string ModelId { get; set; } = default!;
    public string? ApiKey { get; set; }
    public string? OrgId { get; set; }
    public int? Dimensions { get; set; }
}
