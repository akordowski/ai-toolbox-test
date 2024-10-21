using AIToolbox.Options.Agents;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public class AgentsBuilderTests
{
    private readonly AgentOptions _options = new();
    private readonly ServiceCollection _services = [];
    private readonly AgentsBuilder _builder;

    public AgentsBuilderTests()
    {
        _builder = new AgentsBuilder(_options, _services);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var builder = new AgentsBuilder(_options, services);

        // Assert
        builder.Options.Should().Be(_options);
        builder.Services.Should().BeEquivalentTo(services);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(AgentOptions) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Options()
    {
        // Act
        var act = () => new AgentsBuilder(null!, _services);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("No 'AgentOptions' provided. *options*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Services()
    {
        // Act
        var act = () => new AgentsBuilder(_options, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*services*");
    }

    [Fact]
    public void Should_Include_Agent_With_Options()
    {
        // Arrange
        var options = new ChatCompletionAgentOptions();

        // Act
        var result = _builder.IncludeChatCompletionAgent(options);

        // Assert
        result.Should().NotBeNull();
        _options.ChatCompletion.Should().Be(options);
    }

    [Fact]
    public void Should_Include_Agent_With_Null_Options()
    {
        // Act
        var result = _builder.IncludeChatCompletionAgent();

        // Assert
        result.Should().NotBeNull();
        _options.ChatCompletion.Should().NotBeNull();
    }

    [Fact]
    public void Should_Include_Agent_With_Options_Action()
    {
        // Act
        var result = _builder.IncludeChatCompletionAgent(options => options.DataStorage = new ChatCompletionAgentDataStorageOptions());

        // Assert
        result.Should().NotBeNull();
        _options.ChatCompletion.Should().NotBeNull();
        _options.ChatCompletion!.DataStorage.Should().NotBeNull();
    }

    [Fact]
    public void Should_Throw_Exception_When_Include_Agent_With_Null_Options_Action()
    {
        // Act
        var act = () => _builder.IncludeChatCompletionAgent((Action<ChatCompletionAgentOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }
}
