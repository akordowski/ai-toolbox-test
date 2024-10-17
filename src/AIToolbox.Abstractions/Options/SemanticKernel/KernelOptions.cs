namespace AIToolbox.Options.SemanticKernel;

public sealed class KernelOptions
{
    public bool AddLogging { get; set; }
    public AzureOpenAIOptions? AzureOpenAI { get; set; }
    public GoogleAIOptions? GoogleAI { get; set; }
    public HuggingFaceOptions? HuggingFace { get; set; }
    public MistralOptions? Mistral { get; set; }
    public OllamaOptions? Ollama { get; set; }
    public OpenAIOptions? OpenAI { get; set; }
    public VertexAIOptions? VertexAI { get; set; }
    public PluginOptions? Plugins { get; set; }
}
