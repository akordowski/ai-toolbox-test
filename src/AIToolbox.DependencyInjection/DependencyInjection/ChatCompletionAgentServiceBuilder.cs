using AIToolbox.Agents.ChatCompletion;
using AIToolbox.Agents.ChatCompletion.Resources;
using AIToolbox.Agents.ChatCompletion.Services;
using AIToolbox.Data;
using AIToolbox.Options;
using AIToolbox.Options.Agents;
using AIToolbox.Options.Data;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.ChatCompletion;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class ChatCompletionAgentServiceBuilder : IChatCompletionAgentServiceBuilder
{
    public ChatCompletionAgentOptions Options { get; }
    public IServiceCollection Services { get; }

    public ChatCompletionAgentServiceBuilder(
        ChatCompletionAgentOptions options,
        IServiceCollection services)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(ChatCompletionAgentOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));

        Options = options;
        Services = services;

        var chatAgentOptions = new ChatAgentOptions
        {
            ChatHistory = Options.ChatHistory,
            MemorySearch = Options.MemorySearch,
            PromptExecution = Options.PromptExecution
        };

        Services
            .AddSingleton(chatAgentOptions)
            .AddScoped<IChatResource, ChatResource>()
            .AddScoped<IMessageResource, MessageResource>()
            .AddScoped<IParticipantResource, ParticipantResource>()
            .AddScoped<ISettingsResource, SettingsResource>()
            .AddScoped<IPersistentChatAgentService, PersistentChatAgentService>()
            .AddScoped<IChatAgent, ChatAgent>()
            .AddScoped<IPersistentChatAgent, PersistentChatAgent>();

        if (Options.ChatHistoryServiceType == ServiceType.Default)
        {
            Services.AddScoped<IChatHistoryRetriever, ChatHistoryRetriever>();
        }

        if (Options.PromptExecutionSettingsServiceType == ServiceType.Default)
        {
            Services.AddScoped<IPromptExecutionSettingsRetriever, PromptExecutionSettingsRetriever>();
        }
    }

    public IChatCompletionAgentServiceBuilder WithSemanticTextMemoryRetriever()
    {
        var memoryProviderType = typeof(IMemoryProvider);
        var isRegistered = Services.Any(descriptor => descriptor.ServiceType == memoryProviderType);

        if (!isRegistered)
        {
            const string message = $"To use the '{nameof(SemanticTextMemoryRetriever)}' with the chat agent, " +
                                   $"include the memory first using the '{nameof(IKernelServiceBuilder.AddMemory)}()' method.";

            throw new InvalidOperationException(message);
        }

        Services.AddScoped<ISemanticTextMemoryRetriever, SemanticTextMemoryRetriever>();

        return this;
    }

    public IChatCompletionAgentServiceBuilder WithSimpleDataStorage(SimpleDataStorageOptions? options = null)
    {
        if (options is not null)
        {
            Options.DataStorage ??= new ChatCompletionAgentDataStorageOptions();
            Options.DataStorage.SimpleDataStorage = options;
        }

        Verify.ThrowInvalidOperationExceptionIfNull(
            Options.DataStorage?.SimpleDataStorage,
            $"No '{nameof(SimpleDataStorageOptions)}' provided.");

        Services
            .AddSingleton(Options.DataStorage!.SimpleDataStorage!)
            .AddScoped<IDataStorage, SimpleDataStorage>();

        return this;
    }

    public IChatCompletionAgentServiceBuilder WithSimpleDataStorage(Action<SimpleDataStorageOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        Options.DataStorage ??= new ChatCompletionAgentDataStorageOptions();
        Options.DataStorage.SimpleDataStorage ??= new SimpleDataStorageOptions();

        optionsAction(Options.DataStorage.SimpleDataStorage);

        return WithSimpleDataStorage(Options.DataStorage.SimpleDataStorage);
    }
}
