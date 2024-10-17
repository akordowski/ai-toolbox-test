using AIToolbox.Agents.ChatCompletion.Models;

namespace AIToolbox.Agents.ChatCompletion.Resources;

public interface IMessageResource : IResource<Message>
{
    Task AddOrUpdateRangeAsync(
        IEnumerable<Message> values,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Message>> GetAllByChatIdAsync(
        string chatId,
        CancellationToken cancellationToken = default);
}
