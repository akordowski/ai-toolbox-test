using AIToolbox.Agents.ChatCompletion.Models;

namespace AIToolbox.Agents.ChatCompletion.Resources;

public interface ISettingsResource : IResource<Settings>
{
    Task<Settings?> GetByChatIdAsync(
        string chatId,
        CancellationToken cancellationToken = default);
}
