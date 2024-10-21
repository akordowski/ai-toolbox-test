using AIToolbox.Options.Agents;

namespace AIToolbox.DependencyInjection;

public interface IIncludeChatCompletionAgent
{
    IChatCompletionAgentBuilder IncludeChatCompletionAgent(ChatCompletionAgentOptions? options = null);
    IChatCompletionAgentBuilder IncludeChatCompletionAgent(Action<ChatCompletionAgentOptions> optionsAction);
}
