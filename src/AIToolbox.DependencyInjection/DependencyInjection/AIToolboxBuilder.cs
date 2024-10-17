using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

internal sealed class AIToolboxBuilder : IAIToolboxBuilder
{
    private readonly IBuilderService _builderService;

    public AIToolboxBuilder(IBuilderService builderService)
    {
        Verify.ThrowIfNull(builderService, nameof(builderService));

        _builderService = builderService;
    }

    public IConnectorsBuilder AddConnectors(ConnectorOptions? options = null) =>
        _builderService.AddConnectors(options);

    public IConnectorsBuilder AddConnectors(Action<ConnectorOptions> optionsAction) =>
        _builderService.AddConnectors(optionsAction);
}
