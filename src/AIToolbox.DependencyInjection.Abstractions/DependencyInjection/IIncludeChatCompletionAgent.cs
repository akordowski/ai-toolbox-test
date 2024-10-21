using AIToolbox.Options.Agents;

namespace AIToolbox.DependencyInjection;

public interface IIncludeChatCompletionAgent
{
    IChatCompletionAgentServiceBuilder IncludeChatCompletionAgent(ChatCompletionAgentOptions? options = null);
    IChatCompletionAgentServiceBuilder IncludeChatCompletionAgent(Action<ChatCompletionAgentOptions> optionsAction);
}
