using AIToolbox.Options.Agents;
using AIToolbox.Options.Data;

namespace AIToolbox.DependencyInjection;

public interface IChatCompletionAgentBuilder : IBuilder<ChatCompletionAgentOptions>
{
    IChatCompletionAgentBuilder WithSemanticTextMemoryRetriever();
    IChatCompletionAgentBuilder WithSimpleDataStorage(SimpleDataStorageOptions? options = null);
    IChatCompletionAgentBuilder WithSimpleDataStorage(Action<SimpleDataStorageOptions> optionsAction);
}
