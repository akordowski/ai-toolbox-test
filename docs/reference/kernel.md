# Kernel

AI Toolbox supports Semantic Kernel functionality. To add the kernel to the AI Toolbox use the `AddKernel()` method. This method is required.

## Configuration

The `AddKernel()` method provides options and methods to configure the Semantic Kernel.

> [!TIP]
> For kernel configuration options please check the [`AIToolbox.Options.SemanticKernel`](/api/AIToolbox.Options.SemanticKernel.html) namespace reference.

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
    .WithCustomAIServiceSelector(new CustomAIServiceSelector())
    .WithCustomFunctionInvocationFilter(new CustomFunctionInvocationFilter());
```
