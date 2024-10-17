using AIToolbox.Agents.ChatCompletion.Models;
using AIToolbox.Data;
using Microsoft.Extensions.Logging;

namespace AIToolbox.Agents.ChatCompletion.Resources;

public sealed class MessageResource : IMessageResource
{
    private readonly IDataStorage _dataStorage;
    private readonly ILogger<MessageResource> _logger;

    public MessageResource(
        IDataStorage dataStorage,
        ILogger<MessageResource> logger)
    {
        ArgumentNullException.ThrowIfNull(dataStorage, nameof(dataStorage));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        _dataStorage = dataStorage;
        _logger = logger;
    }

    public async Task AddOrUpdateAsync(Message value, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        ArgumentException.ThrowIfNullOrWhiteSpace(value.Id, nameof(value.Id));

        await _dataStorage.AddOrUpdateAsync(value, v => v.Id, cancellationToken).ConfigureAwait(false);
        _logger.LogDebug("Message with id {Id} added or updated for chat with id {ChatId}.", value.Id, value.ChatId);
    }

    public async Task AddOrUpdateRangeAsync(IEnumerable<Message> values, CancellationToken cancellationToken = default)
    {
        await _dataStorage.AddOrUpdateRangeAsync(values, v => v.Id, cancellationToken).ConfigureAwait(false);
        _logger.LogDebug("Add or update messages.");
    }

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        Task.Run(() => _dataStorage.AsQueryable<Chat>().Any(), cancellationToken);

    public Task<bool> AnyAsync(Func<Message, bool> predicate, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        var result = _dataStorage.AsQueryable<Message>().Any(predicate);
        return Task.FromResult(result);
    }

    public async Task<Message?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id, nameof(id));
        return await _dataStorage.FindAsync<Message>(id, cancellationToken).ConfigureAwait(false);
    }

    public Task<IEnumerable<Message>> GetAllByChatIdAsync(string chatId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chatId, nameof(chatId));
        return Task.Run(
            () => (IEnumerable<Message>)_dataStorage.AsQueryable<Message>().Where(v => v.ChatId == chatId),
            cancellationToken);
    }

    public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id, nameof(id));

        await _dataStorage.RemoveAsync<Message>(id, cancellationToken).ConfigureAwait(false);
        _logger.LogDebug("Message with id {Id} removed.", id);
    }
}
