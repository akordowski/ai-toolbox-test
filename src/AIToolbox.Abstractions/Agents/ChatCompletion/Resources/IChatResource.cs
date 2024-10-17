using AIToolbox.Agents.ChatCompletion.Models;

namespace AIToolbox.Agents.ChatCompletion.Resources;

public interface IChatResource : IResource<Chat>
{
    Task<IEnumerable<Chat>> GetAllAsync(CancellationToken cancellationToken = default);
}
