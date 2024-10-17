using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

public interface IAddConnectors
{
    IConnectorsBuilder AddConnectors(ConnectorOptions? options = null);
    IConnectorsBuilder AddConnectors(Action<ConnectorOptions> optionsAction);
}
