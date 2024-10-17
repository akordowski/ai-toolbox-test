namespace AIToolbox.Options.SemanticKernel;

public sealed class TextEmbeddingGenerationOptions
{
    public AzureOpenAITextEmbeddingGenerationMemoryOptions? AzureOpenAI { get; set; }
    public OllamaTextEmbeddingGenerationMemoryOptions? Ollama { get; set; }
    public OpenAITextEmbeddingGenerationMemoryOptions? OpenAI { get; set; }
}
