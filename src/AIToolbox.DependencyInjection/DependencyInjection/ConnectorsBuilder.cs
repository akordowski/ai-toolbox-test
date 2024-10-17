using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class ConnectorsBuilder : IConnectorsBuilder
{
    public ConnectorOptions Options { get; }
    public IServiceCollection Services { get; }

    private readonly IBuilderService _builderService;

    public ConnectorsBuilder(
        ConnectorOptions options,
        IServiceCollection services,
        IBuilderService builderService)
    {
        Verify.ThrowIfNull(options, nameof(options));
        Verify.ThrowIfNull(services, nameof(services));
        Verify.ThrowIfNull(builderService, nameof(builderService));

        Options = options;
        Services = services;

        _builderService = builderService;
    }

    public IKernelBuilder AddKernel(KernelOptions? options = null) =>
        _builderService.AddKernel(options);

    public IKernelBuilder AddKernel(Action<KernelOptions> optionsAction) =>
        _builderService.AddKernel(optionsAction);
}
