using Microsoft.SemanticKernel;

namespace AIToolbox.Agents.ChatCompletion;

public interface IChatAgent
{
    IReadOnlyList<ChatMessageContent> ChatMessageContents { get; }

    Task<IReadOnlyList<ChatMessageContent>> SendMessageAsync(
        string message,
        string? memoryCollection = null,
        CancellationToken cancellationToken = default);

    IAsyncEnumerable<StreamingChatMessageContent> SendMessageAsStreamAsync(
        string message,
        string? memoryCollection = null,
        CancellationToken cancellationToken = default);
}
