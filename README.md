# AI Toolbox

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

AI Toolbox is a .NET SDK that provides an extensible toolset for .NET developers to build AI driven applications. It elevates the power of [Semantic Kernel](https://github.com/microsoft/semantic-kernel) by using a configuration driven approach, which allows adding AI capabilities to .NET applications in not time.

**Features:**

- Fluent [configuration](https://akordowski.github.io/ai-toolbox/reference/configuration.html) API
- [Semantic Kernel](https://github.com/microsoft/semantic-kernel) support
  - [AI](https://akordowski.github.io/ai-toolbox/reference/connectors.html#packages) and [memory](https://akordowski.github.io/ai-toolbox/reference/memory.html#packages) connector support
  - [Plugin](http://localhost:8080/features/plugins.html) support
- [Agents](https://akordowski.github.io/ai-toolbox/features/agents/chat-agent.html)
- [Data persistence](https://akordowski.github.io/ai-toolbox/features/data-persistence.html)

**Planned Features:**

- [Kernel Memory](https://github.com/microsoft/kernel-memory) support
- Token calculation
- EF Core data storage connector
- ASP.NET WebApi service
- Docker images
- and much more ...

## Documentation

- [Getting started](https://akordowski.github.io/ai-toolbox/docs/getting-started.html)
- [Reference](https://akordowski.github.io/ai-toolbox/reference/configuration.html)
- [API reference](https://akordowski.github.io/ai-toolbox/api)

## Example

```csharp
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
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
            .IncludeChatCompletionAgent()
            .WithSemanticTextMemoryRetriever();
    })
    .Build();

var agent = host.Services.GetRequiredService<IChatAgent>();

Console.WriteLine();
Console.WriteLine("Chat agent");
Console.WriteLine("-----------------------");
Console.WriteLine("Press Ctrl+C to quit the conversation");
Console.WriteLine();

while (true)
{
    Console.Write("User > ");

    var message = Console.ReadLine()!;
    var first = true;

    await foreach (var response in agent.SendMessageAsStreamAsync(message))
    {
        if (first)
        {
            Console.Write("Assistant > ");
            first = false;
        }

        Console.Write(response.Content);
    }

    Console.WriteLine();
    Console.WriteLine();
}

await host.RunAsync();
```

## Credits

Icon by [Freepik](https://www.flaticon.com/free-icon/artificial-intelligence_4132678)
