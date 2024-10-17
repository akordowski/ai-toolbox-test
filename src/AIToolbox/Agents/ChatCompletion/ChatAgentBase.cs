using AIToolbox.Options.Agents.ChatCompletion;
using AIToolbox.SemanticKernel;
using AIToolbox.SemanticKernel.ChatCompletion;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.Agents.ChatCompletion;

public abstract class ChatAgentBase
{
    private readonly IChatHistoryRetriever _chatHistoryRetriever;
    private readonly IPromptExecutionSettingsRetriever? _promptExecutionSettingsRetriever;
    private readonly ISemanticTextMemoryRetriever? _semanticTextMemoryRetriever;
    private readonly ChatAgentOptions? _options;
    private readonly Kernel _kernel;

    protected ChatAgentBase(
        IKernelProvider kernelProvider,
        IChatHistoryRetriever chatHistoryRetriever,
        IPromptExecutionSettingsRetriever? promptExecutionSettingsRetriever = null,
        ISemanticTextMemoryRetriever? semanticTextMemoryRetriever = null,
        ChatAgentOptions? options = null)
    {
        _chatHistoryRetriever = chatHistoryRetriever;
        _promptExecutionSettingsRetriever = promptExecutionSettingsRetriever;
        _semanticTextMemoryRetriever = semanticTextMemoryRetriever;
        _options = options;

#pragma warning disable CA1062 // Validate arguments of public methods
        _kernel = kernelProvider.GetKernel();
#pragma warning restore CA1062 // Validate arguments of public methods
    }

    protected Task<IReadOnlyList<ChatMessageContent>> GetChatMessageContentsAsync(
        List<MemoryQueryResult> memories,
        List<ChatMessageContent> messages,
        CancellationToken cancellationToken)
    {
        var chatCompletionService = GetChatCompletionService();
        var chatHistory = GetChatHistory(memories, messages);
        var promptExecutionSettings = GetPromptExecutionSettings(chatCompletionService.GetType());

        return chatCompletionService.GetChatMessageContentsAsync(
            chatHistory,
            promptExecutionSettings,
            _kernel,
            cancellationToken);
    }

    protected IAsyncEnumerable<StreamingChatMessageContent> GetStreamingChatMessageContentsAsync(
        List<MemoryQueryResult> memories,
        List<ChatMessageContent> messages,
        CancellationToken cancellationToken)
    {
        var chatCompletionService = GetChatCompletionService();
        var chatHistory = GetChatHistory(memories, messages);
        var promptExecutionSettings = GetPromptExecutionSettings(chatCompletionService.GetType());

        return chatCompletionService.GetStreamingChatMessageContentsAsync(
            chatHistory,
            promptExecutionSettings,
            _kernel,
            cancellationToken);
    }

    protected async Task<List<MemoryQueryResult>> GetMemoriesAsync(
        string? collection,
        string? query,
        CancellationToken cancellationToken)
    {
        if (_semanticTextMemoryRetriever is null ||
            string.IsNullOrWhiteSpace(collection) ||
            string.IsNullOrWhiteSpace(query))
        {
            return [];
        }

        var options = _options?.MemorySearch;
        var limit = options?.Limit ?? 1;
        var minRelevanceScore = options?.MinRelevanceScore ?? 0.7;

        return await _semanticTextMemoryRetriever
            .SearchMemoriesAsync(
                collection,
                query,
                limit,
                minRelevanceScore,
                false,
                _kernel,
                cancellationToken)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    private IChatCompletionService GetChatCompletionService() =>
        _kernel.GetRequiredService<IChatCompletionService>();

    private ChatHistory GetChatHistory(
        List<MemoryQueryResult> memories,
        List<ChatMessageContent> messages) =>
        _chatHistoryRetriever.GetChatHistory(
            memories,
            messages,
            _options?.ChatHistory);

    private PromptExecutionSettings? GetPromptExecutionSettings(Type serviceType) =>
        _promptExecutionSettingsRetriever?.GetPromptExecutionSettings(
            _kernel,
            serviceType,
            _options?.PromptExecution);
}
