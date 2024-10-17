namespace AIToolbox.Options.Connectors;

public sealed class ConnectorOptions
{
    public AzureOpenAIConnectorOptions? AzureOpenAI { get; set; }
    public GoogleConnectorOptions? Google { get; set; }
    public HuggingFaceConnectorOptions? HuggingFace { get; set; }
    public MistralAIConnectorOptions? MistralAI { get; set; }
    public OllamaConnectorOptions? Ollama { get; set; }
    public OpenAIConnectorOptions? OpenAI { get; set; }
}
