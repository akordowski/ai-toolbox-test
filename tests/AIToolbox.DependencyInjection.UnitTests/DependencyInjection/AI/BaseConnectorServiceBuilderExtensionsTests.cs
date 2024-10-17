using AIToolbox.Options.Connectors;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection;

public abstract class BaseConnectorServiceBuilderExtensionsTests
{
    protected ConnectorOptions Options { get; } = new();
    protected ServiceCollection Services { get; } = [];
    protected Mock<IBuilderService> BuilderServiceMock { get; } = new();
    protected IConnectorsBuilder Builder { get; }

    protected BaseConnectorServiceBuilderExtensionsTests()
    {
        Builder = new ConnectorsBuilder(Options, Services, BuilderServiceMock.Object);
    }
}
