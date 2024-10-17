using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

internal static class StreamingChatMessageContentExtensions
{
    public static string? GetFinishReason(this StreamingChatMessageContent content)
    {
        if (content.Metadata is null ||
            !content.Metadata.TryGetValue("FinishReason", out var finishReason) ||
            string.IsNullOrWhiteSpace(finishReason as string))
        {
            return null;
        }

        return finishReason as string;
    }

    public static bool IsLastMessage(this StreamingChatMessageContent content) =>
        content.GetFinishReason()?.ToUpperInvariant() == "STOP";
}
