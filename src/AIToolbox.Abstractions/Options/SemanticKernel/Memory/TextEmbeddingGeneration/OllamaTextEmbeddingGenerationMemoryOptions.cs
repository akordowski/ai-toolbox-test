namespace AIToolbox.Options.SemanticKernel;

public sealed class OllamaTextEmbeddingGenerationMemoryOptions
{
    public string ModelId { get; set; } = default!;
    public string? Endpoint { get; set; }
}
