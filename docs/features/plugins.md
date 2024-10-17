# Plugins

AI Toolbox provides support for Semantic Kernel plugin functionality. You can configure the plugin usage by providing [Plugin options](/api/AIToolbox.Options.SemanticKernel.PluginOptions.html) in the [Kernel options](/api/AIToolbox.Options.SemanticKernel.KernelOptions.html).

```csharp
var kernelOptions = new KernelOptions()
{
    Plugins = new PluginOptions
    {
        AssemblyPlugins = [],
        DirectoryPlugins = [],
        TypePlugins = []
    }
}
```

## Assembly plugin options

The [`AssemblyPluginOptions`](/api/AIToolbox.Options.SemanticKernel.AssemblyPluginOptions.html) are especially for the usage with a `appsettings.json` configuration. With this options you can provide a assembly-qualified name of the plugin type you want to use.

```json
{
    "AIToolbox": {
        "Kernel": {
            "Plugins": {
                "AssemblyPlugins": [
                    {
                        "AssemblyQualifiedName": "Microsoft.SemanticKernel.Plugins.Core.MathPlugin, Microsoft.SemanticKernel.Plugins.Core"
                    },
                    {
                        "AssemblyQualifiedName": "Microsoft.SemanticKernel.Plugins.Core.TimePlugin, Microsoft.SemanticKernel.Plugins.Core"
                    }
                ]
            }
        }
    }
}
```

You can use this option also in code.

```csharp
var kernelOptions = new KernelOptions
{
    Plugins = new PluginOptions
    {
        AssemblyPlugins =
        [
            new AssemblyPluginOptions
            {
                AssemblyQualifiedName = "Microsoft.SemanticKernel.Plugins.Core.MathPlugin, Microsoft.SemanticKernel.Plugins.Core"
            },
            new AssemblyPluginOptions
            {
                AssemblyQualifiedName = "Microsoft.SemanticKernel.Plugins.Core.TimePlugin, Microsoft.SemanticKernel.Plugins.Core"
            }
        ]
    }
};
```

## Directory plugin options

The [`DirectoryPluginOptions`](/api/AIToolbox.Options.SemanticKernel.DirectoryPluginOptions.html) are for defining directory plugins.

```csharp
var kernelOptions = new KernelOptions
{
    Plugins = new PluginOptions
    {
        DirectoryPlugins =
        [
            new DirectoryPluginOptions
            {
                PluginDirectory = "plugins/math/"
            },
            new DirectoryPluginOptions
            {
                PluginDirectory = "plugins/time/"
            }
        ]
    }
};
```

## Type plugin options

The [`TypePluginOptions`](/api/AIToolbox.Options.SemanticKernel.TypePluginOptions.html) are for defining plugins by class type.

```csharp
var kernelOptions = new KernelOptions
{
    Plugins = new PluginOptions
    {
        TypePlugins =
        [
            new TypePluginOptions
            {
                PluginType = typeof(Microsoft.SemanticKernel.Plugins.Core.MathPlugin)
            },
            new TypePluginOptions
            {
                PluginType = typeof(Microsoft.SemanticKernel.Plugins.Core.TimePlugin)
            }
        ]
    }
};
```

## Example

```csharp
services
    .AddAIToolbox()
    .AddConnectors()
    .IncludeOllamaConnector(options => ...)
    .AddKernel(options =>
    {
        options.Plugins = new PluginOptions
        {
            AssemblyPlugins =
            [
                new AssemblyPluginOptions
                {
                    AssemblyQualifiedName = "Microsoft.SemanticKernel.Plugins.Core.MathPlugin, Microsoft.SemanticKernel.Plugins.Core"
                },
                new AssemblyPluginOptions
                {
                    AssemblyQualifiedName = "Microsoft.SemanticKernel.Plugins.Core.TimePlugin, Microsoft.SemanticKernel.Plugins.Core"
                }
            ],
            DirectoryPlugins =
            [
                new DirectoryPluginOptions
                {
                    PluginDirectory = "plugins/math/"
                },
                new DirectoryPluginOptions
                {
                    PluginDirectory = "plugins/time/"
                }
            ],
            TypePlugins =
            [
                new TypePluginOptions
                {
                    PluginType = typeof(Microsoft.SemanticKernel.Plugins.Core.MathPlugin)
                },
                new TypePluginOptions
                {
                    PluginType = typeof(Microsoft.SemanticKernel.Plugins.Core.TimePlugin)
                }
            ]
        }
    })
    .AddAgents()
    .IncludeChatCompletionAgent();
```
