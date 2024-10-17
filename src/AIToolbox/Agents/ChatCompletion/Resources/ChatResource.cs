using AIToolbox.Agents.ChatCompletion.Models;
using AIToolbox.Data;
using Microsoft.Extensions.Logging;

namespace AIToolbox.Agents.ChatCompletion.Resources;

public sealed class ChatResource : IChatResource
{
    private readonly IDataStorage _dataStorage;
    private readonly ILogger<ChatResource> _logger;

    public ChatResource(
        IDataStorage dataStorage,
        ILogger<ChatResource> logger)
    {
        ArgumentNullException.ThrowIfNull(dataStorage, nameof(dataStorage));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        _dataStorage = dataStorage;
        _logger = logger;
    }

    public async Task AddOrUpdateAsync(Chat value, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        ArgumentException.ThrowIfNullOrWhiteSpace(value.Id, nameof(value.Id));

        await _dataStorage.AddOrUpdateAsync(value, v => v.Id, cancellationToken).ConfigureAwait(false);
        _logger.LogDebug("Chat with id {Id} added or updated.", value.Id);
    }

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        Task.Run(() => _dataStorage.AsQueryable<Chat>().Any(), cancellationToken);

    public Task<bool> AnyAsync(Func<Chat, bool> predicate, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
        return Task.Run(() => _dataStorage.AsQueryable<Chat>().Any(predicate), cancellationToken);
    }

    public async Task<Chat?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id, nameof(id));
        return await _dataStorage.FindAsync<Chat>(id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<IEnumerable<Chat>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _dataStorage.AsAsyncEnumerable<Chat>().ToListAsync(cancellationToken).ConfigureAwait(false);

    public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id, nameof(id));

        await _dataStorage.RemoveAsync<Chat>(id, cancellationToken).ConfigureAwait(false);
        _logger.LogDebug("Chat with id {Id} removed.", id);
    }
}
