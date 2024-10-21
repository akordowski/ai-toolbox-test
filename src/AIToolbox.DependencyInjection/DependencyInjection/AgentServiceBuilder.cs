using AIToolbox.Options.Agents;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable InvertIf
namespace AIToolbox.DependencyInjection;

internal sealed class AgentServiceBuilder : IAgentServiceBuilder
{
    public AgentOptions Options { get; }
    public IServiceCollection Services { get; }

    public AgentServiceBuilder(
        AgentOptions options,
        IServiceCollection services)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(AgentOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));

        Options = options;
        Services = services;

        Services.AddSingleton(Options);
    }

    public IChatCompletionAgentServiceBuilder IncludeChatCompletionAgent(ChatCompletionAgentOptions? options = null)
    {
        Options.ChatCompletion ??= new ChatCompletionAgentOptions();

        if (options is not null)
        {
            Options.ChatCompletion = options;
        }

        return new ChatCompletionAgentServiceBuilder(Options.ChatCompletion!, Services);
    }

    public IChatCompletionAgentServiceBuilder IncludeChatCompletionAgent(Action<ChatCompletionAgentOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        Options.ChatCompletion ??= new ChatCompletionAgentOptions();
        optionsAction(Options.ChatCompletion);

        return IncludeChatCompletionAgent(Options.ChatCompletion!);
    }
}
