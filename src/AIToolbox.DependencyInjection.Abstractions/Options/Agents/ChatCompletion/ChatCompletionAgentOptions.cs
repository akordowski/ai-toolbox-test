using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.Options.Agents;

public sealed class ChatCompletionAgentOptions
{
    public ChatCompletionAgentDataStorageOptions? DataStorage { get; set; }
    public ChatHistoryOptions? ChatHistory { get; set; }
    public MemorySearchOptions? MemorySearch { get; set; }
    public PromptExecutionOptions? PromptExecution { get; set; }
    public ServiceType ChatHistoryServiceType { get; set; } = ServiceType.Default;
    public ServiceType PromptExecutionSettingsServiceType { get; set; } = ServiceType.Default;
}
