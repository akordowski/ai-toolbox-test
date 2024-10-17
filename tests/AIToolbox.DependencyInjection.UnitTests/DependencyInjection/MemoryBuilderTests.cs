using AIToolbox.Options;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection;

public class MemoryBuilderTests
{
    private readonly MemoryOptions _options = new();
    private readonly ServiceCollection _services = [];
    private readonly Mock<IBuilderService> _builderServiceMock = new();
    private readonly MemoryBuilder _builder;

    public MemoryBuilderTests()
    {
        _builder = new MemoryBuilder(_options, _services, _builderServiceMock.Object);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var builder = new MemoryBuilder(_options, services, _builderServiceMock.Object);

        // Assert
        builder.Options.Should().Be(_options);
        builder.Services.Should().BeEquivalentTo(services);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(MemoryOptions) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMemoryProvider) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Options()
    {
        // Act
        var act = () => new MemoryBuilder(null!, _services, _builderServiceMock.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("No 'MemoryOptions' provided. *options*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Services()
    {
        // Act
        var act = () => new MemoryBuilder(_options, null!, _builderServiceMock.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*services*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_BuilderService()
    {
        // Act
        var act = () => new MemoryBuilder(_options, _services, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*builderService*");
    }

    [Fact]
    public void Should_Add_Agents_With_Options()
    {
        // Arrange
        var options = new AgentOptions();
        _builderServiceMock
            .Setup(o => o.AddAgents(options))
            .Returns(Mock.Of<IAgentsBuilder>());

        // Act
        var result = _builder.AddAgents(options);

        // Assert
        result.Should().NotBeNull();
        _builderServiceMock.Verify(o => o.AddAgents(options), Times.Once);
    }

    [Fact]
    public void Should_Add_Agents_With_Options_Action()
    {
        // Arrange
        Action<AgentOptions> optionsAction = _ => { };
        _builderServiceMock
            .Setup(o => o.AddAgents(optionsAction))
            .Returns(Mock.Of<IAgentsBuilder>());

        // Act
        var result = _builder.AddAgents(optionsAction);

        // Assert
        result.Should().NotBeNull();
        _builderServiceMock.Verify(o => o.AddAgents(optionsAction), Times.Once);
    }
}
