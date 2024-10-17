using AIToolbox.Agents.ChatCompletion.Models;
using Microsoft.SemanticKernel;

namespace AIToolbox.Agents.ChatCompletion;

public interface IPersistentChatAgent
{
    bool IsInitialized { get; }

    IReadOnlyList<ChatMessageContent> ChatMessageContents { get; }

    Chat? Chat { get; }

    IReadOnlyList<Message> Messages { get; }

    IReadOnlyList<Participant> Participants { get; }

    Task<Chat> CreateChatAsync(
        string title,
        string userId,
        string userName,
        CancellationToken cancellationToken = default);

    Task<Chat> LoadChatAsync(
        string chatId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ChatMessageContent>> SendMessageAsync(
        string userId,
        string message,
        string? memoryCollection = null,
        CancellationToken cancellationToken = default);

    IAsyncEnumerable<StreamingChatMessageContent> SendMessageAsStreamAsync(
        string userId,
        string message,
        string? memoryCollection = null,
        CancellationToken cancellationToken = default);
}
