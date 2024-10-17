# Agents

To add agents to the AI Toolbox use the `AddAgents()` method. This method is optional.

## Configuration

The `AddAgents()` method provides options and methods to configure agents.

> [!TIP]
> For agents configuration options please check the [`AIToolbox.Options.Agents`](/api/AIToolbox.Options.Agents.ChatCompletion.html) namespace reference.

```csharp
services
    .AddAIToolbox()
    .AddConnectors()
    .IncludeOllamaConnector(options => options.Endpoint = "http://localhost:11434")
    .AddKernel(options =>
    {
        options.Ollama = new OllamaOptions
        {
            ChatCompletion = new OllamaChatCompletionOptions { ModelId = "llama3" }
        };
    })
    .AddMemory(options =>
    {
        options.TextEmbeddingGeneration = new TextEmbeddingGenerationOptions
        {
            Ollama = new OllamaTextEmbeddingGenerationMemoryOptions
            {
                ModelId = "llama3"
            }
        };
    })
    .IncludeSimpleMemoryStore(options =>
    {
        options.Directory = "tmp-sk-memory";
        options.StorageType = StorageType.Persistent;
    })
    .AddAgents()
    .IncludeChatCompletionAgent(options =>
    {
        options.ChatHistory = new ChatHistoryOptions { };
        options.ChatHistoryServiceType = ServiceType.Default;
        options.PromptExecution = new PromptExecutionOptions { };
        options.PromptExecutionSettingsServiceType = ServiceType.Default;
        options.MemorySearch = new MemorySearchOptions { };
        options.DataStorage = new ChatCompletionAgentDataStorageOptions
        {
            SimpleDataStorage = new SimpleDataStorageOptions
            {
                Directory = "tmp-data",
                StorageType = StorageType.Persistent
            }
        };
    });
```
