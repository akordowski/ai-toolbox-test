using AIToolbox.Options.Connectors;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class ConnectorServiceBuilderExtensions
{
    public static IConnectorsBuilder IncludeMistralAIConnector(
        this IConnectorsBuilder builder,
        MistralAIConnectorOptions? options = null)
    {
        var opt = builder.Options;

        // Connector options are optional
        if (options is not null)
        {
            opt.MistralAI ??= options;
        }

        if (opt.MistralAI is not null)
        {
            builder.Services.AddSingleton(opt.MistralAI);
        }

        builder.Services.AddSingleton<IKernelBuilderConfigurator, MistralAIKernelBuilderConfigurator>();

        return builder;
    }

    public static IConnectorsBuilder IncludeMistralAIConnector(
        this IConnectorsBuilder builder,
        Action<MistralAIConnectorOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new MistralAIConnectorOptions();
        optionsAction(options);

        return builder.IncludeMistralAIConnector(options);
    }
}
