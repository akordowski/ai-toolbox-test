using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

public sealed class PromptExecutionSettingsRetriever : IPromptExecutionSettingsRetriever
{
    public PromptExecutionSettings? GetPromptExecutionSettings(
        Kernel kernel,
        Type serviceType,
        PromptExecutionOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(kernel, nameof(kernel));
        ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));

        if (options is null ||
            kernel.Services is not IKeyedServiceProvider keyedServiceProvider)
        {
            return null;
        }

        var service = keyedServiceProvider.GetKeyedService<IPromptExecutionSettingsMapper>(serviceType);
        var settings = service?.Map(options);

        if (settings is not null || options.Default is null)
        {
            return settings;
        }

        var opt = options.Default;

        return new PromptExecutionSettings
        {
            ServiceId = opt.ServiceId,
            ModelId = opt.ModelId
        };
    }
}
