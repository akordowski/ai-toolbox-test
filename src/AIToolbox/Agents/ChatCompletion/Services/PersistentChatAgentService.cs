using AIToolbox.Agents.ChatCompletion.Resources;
using AIToolbox.Data;
using Microsoft.Extensions.Logging;

namespace AIToolbox.Agents.ChatCompletion.Services;

public sealed class PersistentChatAgentService : IPersistentChatAgentService
{
    public IChatResource Chats { get; }
    public IMessageResource Messages { get; }
    public IParticipantResource Participants { get; }
    public ISettingsResource Settings { get; }

    public PersistentChatAgentService(
        IDataStorage dataStorage,
        ILoggerFactory loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(dataStorage, nameof(dataStorage));
        ArgumentNullException.ThrowIfNull(loggerFactory, nameof(loggerFactory));

        Chats = new ChatResource(
            dataStorage,
            loggerFactory.CreateLogger<ChatResource>());

        Messages = new MessageResource(
            dataStorage,
            loggerFactory.CreateLogger<MessageResource>());

        Participants = new ParticipantResource(
            dataStorage,
            loggerFactory.CreateLogger<ParticipantResource>());

        Settings = new SettingsResource(
            dataStorage,
            loggerFactory.CreateLogger<SettingsResource>());
    }
}
