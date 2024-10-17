using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

public sealed class KernelProvider : IKernelProvider
{
    private readonly KernelOptions _kernelOptions;
    private readonly IEnumerable<IKernelBuilderConfigurator> _configurators;

    public KernelProvider(
        KernelOptions kernelOptions,
        IEnumerable<IKernelBuilderConfigurator> configurators)
    {
        ArgumentNullException.ThrowIfNull(kernelOptions, nameof(kernelOptions));

        _kernelOptions = kernelOptions;
        _configurators = configurators;
    }

    public Kernel GetKernel()
    {
        var builder = Kernel.CreateBuilder();

        ConfigureLogging(builder);

        foreach (var configurator in _configurators)
        {
            configurator.Configure(builder);
        }

        var kernel = builder.Build();

        ImportPlugins(kernel);

        return kernel;
    }

    private void ConfigureLogging(IKernelBuilder builder)
    {
        if (_kernelOptions.AddLogging)
        {
            builder.Services.AddLogging();
        }
    }

    private void ImportPlugins(Kernel kernel)
    {
        var options = _kernelOptions.Plugins;

        if (options is null)
        {
            return;
        }

        kernel.ImportPluginsFromOptions(options);
    }
}
