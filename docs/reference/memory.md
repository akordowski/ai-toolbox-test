# Memory

AI Toolbox supports Semantic Kernel memory functionality. To add the memory to the AI Toolbox use the `AddMemory()` method. This method is optional.

## Memory Store

AI Toolbox provides out of the box a [simple memory store](/api/AIToolbox.SemanticKernel.Memory.SimpleMemoryStore.html), which can store data volatile (memory) or persistent (text file). AI Toolbox provides also [NuGet packages](#packages) for the Semantic Kernel memory store connectors.

## Configuration

The `AddMemory()` method provides options and methods to configure the Semantic Kernel memory.

> [!TIP]
> For memory configuration options please check the [`AIToolbox.Options.SemanticKernel`](/api/AIToolbox.Options.SemanticKernel.html) namespace reference.

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
    });
```

## Packages

AI Toolbox provides following NuGet packages for the Semantic Kernel memory connectors.

- [AIToolbox.SemanticKernel.Connectors.Memory.AzureAISearch](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.AzureAISearch)
- [AIToolbox.SemanticKernel.Connectors.Memory.AzureCosmosDBMongoDB](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.AzureCosmosDBMongoDB)
- [AIToolbox.SemanticKernel.Connectors.Memory.AzureCosmosDBNoSQL](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.AzureCosmosDBNoSQL)
- [AIToolbox.SemanticKernel.Connectors.Memory.Chroma](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.Chroma)
- [AIToolbox.SemanticKernel.Connectors.Memory.DuckDB](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.DuckDB)
- [AIToolbox.SemanticKernel.Connectors.Memory.Kusto](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.Kusto)
- [AIToolbox.SemanticKernel.Connectors.Memory.Milvus](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.Milvus)
- [AIToolbox.SemanticKernel.Connectors.Memory.MongoDB](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.MongoDB)
- [AIToolbox.SemanticKernel.Connectors.Memory.Pinecone](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.Pinecone)
- [AIToolbox.SemanticKernel.Connectors.Memory.Postgres](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.Postgres)
- [AIToolbox.SemanticKernel.Connectors.Memory.Qdrant](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.Qdrant)
- [AIToolbox.SemanticKernel.Connectors.Memory.Redis](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.Redis)
- [AIToolbox.SemanticKernel.Connectors.Memory.Sqlite](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.Sqlite)
- [AIToolbox.SemanticKernel.Connectors.Memory.SqlServer](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.SqlServer)
- [AIToolbox.SemanticKernel.Connectors.Memory.Weaviate](https://www.nuget.org/packages/AIToolbox.SemanticKernel.Connectors.Memory.Weaviate)
