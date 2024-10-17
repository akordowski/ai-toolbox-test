using AIToolbox.Agents.ChatCompletion.Models;

namespace AIToolbox.Agents.ChatCompletion.Resources;

public interface IParticipantResource : IResource<Participant>
{
    Task<IEnumerable<Participant>> GetAllByChatIdAsync(
        string chatId,
        CancellationToken cancellationToken = default);
}
