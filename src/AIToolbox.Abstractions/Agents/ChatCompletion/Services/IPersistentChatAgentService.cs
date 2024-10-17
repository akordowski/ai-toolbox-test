using AIToolbox.Agents.ChatCompletion.Resources;

namespace AIToolbox.Agents.ChatCompletion.Services;

public interface IPersistentChatAgentService
{
    IChatResource Chats { get; }
    IMessageResource Messages { get; }
    IParticipantResource Participants { get; }
    ISettingsResource Settings { get; }
}
