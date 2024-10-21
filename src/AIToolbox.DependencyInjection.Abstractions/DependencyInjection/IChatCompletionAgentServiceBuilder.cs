using AIToolbox.Options.Agents;
using AIToolbox.Options.Data;

namespace AIToolbox.DependencyInjection;

public interface IChatCompletionAgentServiceBuilder : IServiceBuilder<ChatCompletionAgentOptions>
{
    IChatCompletionAgentServiceBuilder WithSemanticTextMemoryRetriever();
    IChatCompletionAgentServiceBuilder WithSimpleDataStorage(SimpleDataStorageOptions? options = null);
    IChatCompletionAgentServiceBuilder WithSimpleDataStorage(Action<SimpleDataStorageOptions> optionsAction);
}
