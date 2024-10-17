using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.Options.Agents.ChatCompletion;

public sealed class ChatAgentOptions
{
    public ChatHistoryOptions? ChatHistory { get; set; }
    public MemorySearchOptions? MemorySearch { get; set; }
    public PromptExecutionOptions? PromptExecution { get; set; }
}