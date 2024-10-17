using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection;

public abstract class BaseMemoryStoreExtensionsTests
{
    protected MemoryOptions Options { get; } = new();
    protected ServiceCollection Services { get; } = [];
    protected Mock<IBuilderService> BuilderServiceMock { get; } = new();
    protected IMemoryBuilder Builder { get; }

    protected BaseMemoryStoreExtensionsTests()
    {
        Builder = new MemoryBuilder(Options, Services, BuilderServiceMock.Object);
    }
}
