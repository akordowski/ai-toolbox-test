using AIToolbox.Options;

namespace AIToolbox.DependencyInjection;

public interface IIncludeChatCompletionAgent
{
    IChatCompletionAgentBuilder IncludeChatCompletionAgent(ChatCompletionAgentOptions? options = null);
    IChatCompletionAgentBuilder IncludeChatCompletionAgent(Action<ChatCompletionAgentOptions> optionsAction);
}
