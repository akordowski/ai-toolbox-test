# Persistent chat agent

AI Toolbox provides a advanced multi user [`PersistentChatAgent`](/api/AIToolbox.Agents.ChatCompletion.PersistentChatAgent.html) for use cases where data persistence is required. It can be used with or without memory retrieving. It requires a registered [data storage](/features/data-persistence.html#data-storage).

## Example

For a full working example please check the [`PersistentChatAgentDemo`](https://github.com/akordowski/ai-toolbox/tree/main/samples/PersistentChatAgentDemo) project in the repository.

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
            .IncludeSimpleMemoryStore(options => options.StorageType = StorageType.Persistent)
            .AddAgents()
            .IncludeChatCompletionAgent()
            .WithSemanticTextMemoryRetriever()
            .WithSimpleDataStorage(options => options.StorageType = StorageType.Persistent);
    })
    .Build();

var agent = host.Services.GetRequiredService<IPersistentChatAgent>();

const string chatTitle = "My Persistent Chat";
const string userId = "1c277a14-e95f-4347-94d9-d2171479bb70";
const string userName = "John Doe";

await chatAgent.CreateChatAsync(chatTitle, userId, userName);

Console.WriteLine();
Console.WriteLine("Persistent chat agent");
Console.WriteLine("-----------------------");
Console.WriteLine("Press Ctrl+C to quit the conversation");
Console.WriteLine();

while (true)
{
    Console.Write("User > ");

    var message = Console.ReadLine()!;
    var first = true;
    
    await foreach (var response in agent.SendMessageAsStreamAsync(userId, message))
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
