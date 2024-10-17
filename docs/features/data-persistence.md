# Data persistence

AI Toolbox provides for data persistence out of the box a [`SimpleDataStorage`](/api/AIToolbox.Data.SimpleDataStorage.html) and a [`SimpleMemoryStore`](/api/AIToolbox.SemanticKernel.Memory.SimpleMemoryStore.html) class. These classes can store data volatile (memory) or persistent (text file). This allows you to get started with AI Toolbox (and Semantic Kernel) in no time, without having setup a data storage solution first.

## Data storage

For data storage AI Toolbox provides an [`IDataStorage`](/api/AIToolbox.Data.IDataStorage.html) interface. The interface is used by the [`PersistentChatAgentService`](/api/AIToolbox.Agents.ChatCompletion.Services.PersistentChatAgentService.html) to store all chat related data. For more information please check the [`AIToolbox.Agents.ChatCompletion`](/api/AIToolbox.Agents.ChatCompletion.html) namespace reference.

> [!NOTE]
> For data storage currently only [`SimpleDataStorage`](/api/AIToolbox.Data.SimpleDataStorage.html) implementation is available. It is planned to provide an EF Core connector, so the data can be stored on a solution of your choice.

```csharp
services
    .AddAIToolbox()
    .AddConnectors()
    .IncludeOllamaConnector(options => ...)
    .AddKernel(options => ...)
    .AddAgents()
    .IncludeChatCompletionAgent()
    .WithSimpleDataStorage(options =>
    {
        options.StorageType = StorageType.Persistent;
        options.Directory = "tmp-data";
    });
```

## Memory store

For the Semantic Kernel [`IMemoryStore`](https://github.com/microsoft/semantic-kernel/blob/main/dotnet/src/SemanticKernel.Abstractions/Memory/IMemoryStore.cs) interface AI Toolbox provides the [`SimpleMemoryStore`](/api/AIToolbox.SemanticKernel.Memory.SimpleMemoryStore.html) implementation. For other available memory store connectors please check the [NuGet package](/reference/memory.html#packages) overview.

```csharp
services
    .AddAIToolbox()
    .AddConnectors()
    .IncludeOllamaConnector(options => ...)
    .AddKernel(options => ...)
    .AddMemory(options => ...)
    .IncludeSimpleMemoryStore(options => options.StorageType = StorageType.Persistent)
    .AddAgents()
    .IncludeChatCompletionAgent(options =>
    {
        options.StorageType = StorageType.Persistent;
        options.Directory = "tmp-sk-memory";
    })
    .WithSemanticTextMemoryRetriever()
    .WithSimpleDataStorage(options => ...);
```
