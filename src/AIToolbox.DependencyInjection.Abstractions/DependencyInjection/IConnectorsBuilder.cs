using AIToolbox.Options.Connectors;

namespace AIToolbox.DependencyInjection;

public interface IConnectorsBuilder : IBuilder<ConnectorOptions>, IAddKernel
{
}
