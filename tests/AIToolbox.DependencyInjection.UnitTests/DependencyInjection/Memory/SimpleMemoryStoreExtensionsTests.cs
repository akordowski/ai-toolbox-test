using AIToolbox.Options;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public class SimpleMemoryStoreExtensionsTests : BaseMemoryStoreExtensionsTests
{
    [Fact]
    public void Should_Include_SimpleMemoryStore_With_Options()
    {
        // Arrange
        var options = new SimpleMemoryStoreOptions();

        // Act
        var result = Builder.IncludeSimpleMemoryStore(options);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.SimpleMemoryStore.Should().Be(options);

        AssertServices();
    }

    [Fact]
    public void Should_Include_SimpleMemoryStore_With_Null_Options()
    {
        // Arrange
        Options.Store = new MemoryStoreOptions
        {
            SimpleMemoryStore = new SimpleMemoryStoreOptions()
        };

        // Act
        var result = Builder.IncludeSimpleMemoryStore();

        // Assert
        result.Should().Be(Builder);
        Options.Store.SimpleMemoryStore.Should().NotBeNull();

        AssertServices();
    }

    [Fact]
    public void Should_Include_SimpleMemoryStore_With_Options_Action()
    {
        // Act
        var result = Builder.IncludeSimpleMemoryStore(options => options.StorageType = StorageType.Volatile);

        // Assert
        result.Should().Be(Builder);
        Options.Store!.SimpleMemoryStore!.StorageType.Should().Be(StorageType.Volatile);

        AssertServices();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_SimpleMemoryStore_With_Null_Options_Action()
    {
        // Act
        var act = () => Builder.IncludeSimpleMemoryStore((Action<SimpleMemoryStoreOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }

    private void AssertServices()
    {
        Services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryStoreFactory) &&
                                                      descriptor.ImplementationType == typeof(SimpleMemoryStoreFactory) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
