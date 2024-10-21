using AIToolbox.Options.Agents;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

// ReSharper disable InvertIf

internal sealed class AgentsBuilder : IAgentsBuilder
{
    public AgentOptions Options { get; }
    public IServiceCollection Services { get; }

    public AgentsBuilder(
        AgentOptions options,
        IServiceCollection services)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(AgentOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));

        Options = options;
        Services = services;

        Services.AddSingleton(Options);
    }

    public IChatCompletionAgentBuilder IncludeChatCompletionAgent(ChatCompletionAgentOptions? options = null)
    {
        Options.ChatCompletion ??= new ChatCompletionAgentOptions();

        if (options is not null)
        {
            Options.ChatCompletion = options;
        }

        return new ChatCompletionAgentBuilder(Options.ChatCompletion!, Services);
    }

    public IChatCompletionAgentBuilder IncludeChatCompletionAgent(Action<ChatCompletionAgentOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        Options.ChatCompletion ??= new ChatCompletionAgentOptions();
        optionsAction(Options.ChatCompletion);

        return IncludeChatCompletionAgent(Options.ChatCompletion!);
    }
}
