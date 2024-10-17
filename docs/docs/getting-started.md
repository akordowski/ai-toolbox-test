# Getting started

In this example you will learn how to use an AI chat agent by creating a simple console application. For simplicity we will use an Ollama LLM in this example. If you already have access to another LLM provider you can use the corresponding [connector](/reference/connectors.html#packages) instead.

> [!TIP]
> More information on [Ollama](https://ollama.com) you can find [here](https://github.com/ollama/ollama).

## Create an application

1. Create a console application.

```bash
dotnet new console AIToolboxExample
```
2. Add NuGet packages.

```bash
dotnet add package Microsoft.Extensions.Hosting
dotnet add package AIToolbox.DependencyInjection --prerelease
dotnet add package AIToolbox.SemanticKernel.Connectors.Ollama --prerelease
```

3. Add basic code.

```csharp
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => {})
    .Build();

await host.RunAsync();
```

## Add AI Toolbox

Add AI Toolbox to the service collection.  
You can learn more about AI Toolbox configuration [here](/reference/configuration.html).

```csharp
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddAIToolbox();
    }
```

## Add connectors

Include the Ollama connector.  
You can learn more about connectors [here](/reference/connectors.html).

```csharp
services
    .AddAIToolbox()
    .AddConnectors()
    .IncludeOllamaConnector(options => options.Endpoint = "http://localhost:11434");
```

## Add kernel

Add the Semantic Kernel and provide the Ollama configuration.  
You can learn more about kernel [here](/reference/kernel.html).

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
    };
```

## Add agents

Include the chat completion agent.  
You can learn more about agents [here](/reference/agents.html).

```csharp
services
    .AddAIToolbox()
    .AddConnectors()
    .IncludeOllamaConnector(options => ...)
    .AddKernel(options => ...)
    .AddAgents()
    .IncludeChatCompletionAgent();
```

## Send a message

Get the chat agent and send a message.

```csharp
var agent = host.Services.GetRequiredService<IChatAgent>();
var message = Console.ReadLine()!;

await foreach (var response in agent.SendMessageAsStreamAsync(message))
{
    Console.Write(response.Content);
}
```

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
            .AddAgents()
            .IncludeChatCompletionAgent();
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
