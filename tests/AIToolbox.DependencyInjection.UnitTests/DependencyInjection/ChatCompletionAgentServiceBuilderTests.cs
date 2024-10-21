using AIToolbox.Agents.ChatCompletion;
using AIToolbox.Agents.ChatCompletion.Resources;
using AIToolbox.Agents.ChatCompletion.Services;
using AIToolbox.Data;
using AIToolbox.Options;
using AIToolbox.Options.Agents;
using AIToolbox.Options.Data;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.ChatCompletion;
using AIToolbox.SemanticKernel.Memory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AIToolbox.DependencyInjection;

public class ChatCompletionAgentServiceBuilderTests
{
    private readonly ChatCompletionAgentOptions _options = new()
    {
        ChatHistory = new ChatHistoryOptions(),
        MemorySearch = new MemorySearchOptions(),
        PromptExecution = new PromptExecutionOptions()
    };
    private readonly ServiceCollection _services = [];
    private readonly ChatCompletionAgentServiceBuilder _builder;

    public ChatCompletionAgentServiceBuilderTests()
    {
        _builder = new ChatCompletionAgentServiceBuilder(_options, _services);
    }

    [Fact]
    public void Should_Construct_With_Valid_Parameters()
    {
        // Arrange
        _options.ChatHistoryServiceType = ServiceType.Default;
        _options.PromptExecutionSettingsServiceType = ServiceType.Default;

        var services = new ServiceCollection();

        // Act
        var builder = new ChatCompletionAgentServiceBuilder(_options, services);

        // Assert
        builder.Options.Should().Be(_options);
        builder.Services.Should().BeEquivalentTo(services);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(ChatAgentOptions) &&
                                                      descriptor.Lifetime == ServiceLifetime.Singleton);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IChatResource) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IMessageResource) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IParticipantResource) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(ISettingsResource) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IPersistentChatAgentService) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IChatAgent) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IPersistentChatAgent) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IChatHistoryRetriever) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);

        services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IPromptExecutionSettingsRetriever) &&
                                                      descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Options()
    {
        // Act
        var act = () => new ChatCompletionAgentServiceBuilder(null!, _services);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*options*");
    }

    [Fact]
    public void Should_Throw_Exception_When_Constructed_With_Null_Services()
    {
        // Act
        var act = () => new ChatCompletionAgentServiceBuilder(_options, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*services*");
    }

    [Fact]
    public void Should_Not_Add_ChatHistoryRetriever_When_ChatHistoryServiceType_Is_Custom()
    {
        // Arrange
        _options.ChatHistoryServiceType = ServiceType.Custom;
        var services = new ServiceCollection();
        var builder = new ChatCompletionAgentServiceBuilder(_options, services);

        // Act
        // No additional action needed as services are added in the constructor

        // Assert
        services.Should().NotContain(descriptor => descriptor.ServiceType == typeof(IChatHistoryRetriever));
    }

    [Fact]
    public void Should_Not_Add_PromptExecutionSettingsRetriever_When_PromptExecutionSettingsServiceType_Is_Custom()
    {
        // Arrange
        _options.PromptExecutionSettingsServiceType = ServiceType.Custom;
        var services = new ServiceCollection();
        var builder = new ChatCompletionAgentServiceBuilder(_options, services);

        // Act
        // No additional action needed as services are added in the constructor

        // Assert
        services.Should().NotContain(descriptor => descriptor.ServiceType == typeof(IPromptExecutionSettingsRetriever));
    }

    [Fact]
    public void Should_Throw_Exception_When_Add_SemanticTextMemoryRetriever_And_Memory_Is_Not_Registered()
    {
        // Arrange
        const string message = "To use the 'SemanticTextMemoryRetriever' with the chat agent, " +
                               "include the memory first using the 'AddMemory()' method.";

        // Act
        var act = () => _builder.WithSemanticTextMemoryRetriever();

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_SemanticTextMemoryRetriever_When_Memory_Is_Registered()
    {
        // Arrange
        _services.AddSingleton(Mock.Of<IMemoryProvider>());

        // Act
        _builder.WithSemanticTextMemoryRetriever();

        // Assert
        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(ISemanticTextMemoryRetriever) &&
                                                       descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Add_SimpleDataStorage_With_Options()
    {
        // Act
        _builder.WithSimpleDataStorage(new SimpleDataStorageOptions());

        // Assert
        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IDataStorage) &&
                                                       descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Add_SimpleDataStorage_With_Null_Options()
    {
        // Arrange
        _options.DataStorage = new ChatCompletionAgentDataStorageOptions
        {
            SimpleDataStorage = new SimpleDataStorageOptions()
        };

        // Act
        _builder.WithSimpleDataStorage();

        // Assert
        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IDataStorage) &&
                                                       descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Throw_Exception_When_SimpleDataStorageOptions_Not_Provided()
    {
        // Arrange
        const string message = "No 'SimpleDataStorageOptions' provided.";

        // Act
        var act = () => _builder.WithSimpleDataStorage();

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage(message);
    }

    [Fact]
    public void Should_Add_SimpleDataStorage_With_Options_Action()
    {
        // Act
        _builder.WithSimpleDataStorage(options => options.StorageType = StorageType.Volatile);

        // Assert
        _services.Should().ContainSingle(descriptor => descriptor.ServiceType == typeof(IDataStorage) &&
                                                       descriptor.Lifetime == ServiceLifetime.Scoped);
    }

    [Fact]
    public void Should_Throw_Exception_When_Add_SimpleDataStorage_With_Null_Options_Action()
    {
        // Act
        var act = () => _builder.WithSimpleDataStorage((Action<SimpleDataStorageOptions>)null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("*optionsAction*");
    }
}
