# Configuration

AI Toolbox uses a configuration driven approach, which allows adding AI capabilities to .NET applications in not time. The easiest way to set up an application is to use the functionality provided by the `AIToolbox.DependencyInjection` NuGet package.

The `AddAIToolbox()` method is the starting point for the configuration of the application.

> [!NOTE]
> All of the following described configuration options also apply to all other AI Toolbox methods.

## Global configuration

You can provide a global configuration by using the `AddAIToolbox()` method.

> [!IMPORTANT]
> Global configuration is optional. If you do without it then you must provide the configuration on the methods accordingly.

> [!TIP]
> For all configuration options please check the [`AIToolbox.Options`](/api/AIToolbox.Options.AIToolboxOptions.html) namespace reference.

### `appsettings.json` configuration

You can provide a global configuration using an `appsettings.json` file. The root section name `AIToolbox` is a optional default value and can be changed.

```json
{
    "AIToolbox": {
        "Connectors": { },
        "Kernel": { },
        "Memory": { },
        "Agents": { }
    }
}
```

```csharp
host.ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("appsettings.json", true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddAIToolbox(context.Configuration, "AIToolbox");
    });
```

> [!IMPORTANT]
> Empty objects defined in `appsettings.json` like `"Connectors": { }` are not parsed by the `ConfigurationBuilder` and therefore always `null`. At least one property must be provided so that the object can be parsed to a valid options class instance.

### Class configuration

You can provide a global configuration using a options class instance.

```csharp
host.ConfigureServices((_, services) =>
    {
        var options = new AIToolboxOptions
        {
            Connectors = new ConnectorOptions { },
            Kernel = new KernelOptions { },
            Memory = new MemoryOptions { },
            Agents = new AgentOptions { }
        };
        
        services.AddAIToolbox(options);
    });
```

### Inline configuration

You can provide a global configuration using the methods inline options action.

```csharp
host.ConfigureServices((_, services) =>
    {
        services.AddAIToolbox(options =>
        {
            options.Connectors = new ConnectorOptions { };
            options.Kernel = new KernelOptions { };
            options.Memory = new MemoryOptions { };
            options.Agents = new AgentOptions { };
        });
    });
```

## Methods configuration

If the [global configuration](#global-configuration) is not provided, then each method must be configured accordingly. Same as the `AddAIToolbox()` method, all other methods can be configured by providing an class or inline configuration.

### Class configuration

```csharp
host.ConfigureServices((_, services) =>
    {
        var connectorOptions = new ConnectorOptions { };
        var kernelOptions = new KernelOptions { };
        var memoryOptions = new MemoryOptions { };
        var agentOptions = new AgentOptions { };
        
        services
            .AddAIToolbox()
            .AddConnectors(connectorOptions)
            .AddKernel(kernelOptions)
            .AddMemory(memoryOptions)
            .AddAgents(agentOptions);
    });
```

### Inline configuration

```csharp
host.ConfigureServices((_, services) =>
    {
        services
            .AddAIToolbox()
            .AddConnectors(options => ...)
            .AddKernel(options => ...)
            .AddMemory(options => ...)
            .AddAgents(options => ...);
    });
```
