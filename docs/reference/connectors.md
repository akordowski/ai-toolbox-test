# Connectors

AI Toolbox supports available Semantic Kernel connectors. To add connectors to the AI Toolbox use the `AddConnectors()` method. This method is required. After adding the respective connector [NuGet package](#packages) you can include the connector.

## Configuration

The `AddConnectors()` method provides a convenient way to configure the connector connection settings globally.

> [!TIP]
> For connectors configuration options please check the [`AIToolbox.Options.Connectors`](/api/AIToolbox.Options.Connectors.html) namespace reference.

```csharp
services
    .AddAIToolbox()
    .AddConnectors(options => options.Ollama = new OllamaConnectorOptions { })
    .IncludeOllamaConnector();
```

```csharp
services
    .AddAIToolbox()
    .AddConnectors()
    .IncludeOllamaConnector(options => ...);
```

### With global configuration

By providing the connector configuration globally AI Toolbox takes care of configuring the AI provider services automatically.

```csharp
services
    .AddAIToolbox()
    .AddConnectors()
    .IncludeOllamaConnector(options => options.Endpoint = "http://localhost:11434")
    .AddKernel(options =>
    {
        options.Ollama = new OllamaOptions
        {
            ChatCompletion = new OllamaChatCompletionOptions { ModelId = "llama3" },
            TextEmbeddingGeneration = new OllamaTextEmbeddingGenerationOptions { ModelId = "llama3" },
            TextGeneration = new OllamaTextGenerationOptions { ModelId = "llama3" }
        };
    })
```

### Without global configuration

If you don't provide the connector configuration globally you must configure the AI provider services accordingly.

```csharp
services
    .AddAIToolbox()
    .AddConnectors()
    .IncludeOllamaConnector()
    .AddKernel(options =>
    {
        options.Ollama = new OllamaOptions
        {
            ChatCompletion = new OllamaChatCompletionOptions
            {
                Endpoint = "http://localhost:11434",
                ModelId = "llama3"
            },
            TextEmbeddingGeneration = new OllamaTextEmbeddingGenerationOptions
            {
                Endpoint = "http://localhost:11434",
                ModelId = "llama3"
            },
            TextGeneration = new OllamaTextGenerationOptions
            {
                Endpoint = "http://localhost:11434",
                ModelId = "llama3"
            }
        };
    })
```

## Packages

AI Toolbox provides following NuGet packages for the Semantic Kernel connectors.

- [AIToolbox.SemanticKernel.Connectors.AzureOpenAI](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.AzureOpenAI/)
- [AIToolbox.SemanticKernel.Connectors.Google](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Google)
- [AIToolbox.SemanticKernel.Connectors.HuggingFace](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.HuggingFace)
- [AIToolbox.SemanticKernel.Connectors.MistralAI](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.MistralAI)
- [AIToolbox.SemanticKernel.Connectors.Ollama](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Ollama)
- [AIToolbox.SemanticKernel.Connectors.OpenAI](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.OpenAI)
