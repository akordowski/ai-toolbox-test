using AIToolbox.Agents.ChatCompletion.Models;
using AIToolbox.Data;
using Microsoft.Extensions.Logging;

namespace AIToolbox.Agents.ChatCompletion.Resources;

public sealed class SettingsResource : ISettingsResource
{
    private readonly IDataStorage _dataStorage;
    private readonly ILogger<SettingsResource> _logger;

    public SettingsResource(
        IDataStorage dataStorage,
        ILogger<SettingsResource> logger)
    {
        ArgumentNullException.ThrowIfNull(dataStorage, nameof(dataStorage));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        _dataStorage = dataStorage;
        _logger = logger;
    }

    public async Task AddOrUpdateAsync(Settings value, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        ArgumentException.ThrowIfNullOrWhiteSpace(value.Id, nameof(value.Id));

        await _dataStorage.AddOrUpdateAsync(value, v => v.Id, cancellationToken).ConfigureAwait(false);
        _logger.LogDebug("Settings with id {Id} added or updated for chat with id {ChatId}.", value.Id, value.ChatId);
    }

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        Task.Run(() => _dataStorage.AsQueryable<Chat>().Any(), cancellationToken);

    public Task<bool> AnyAsync(Func<Settings, bool> predicate, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));

        var result = _dataStorage.AsQueryable<Settings>().Any(predicate);
        return Task.FromResult(result);
    }

    public async Task<Settings?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id, nameof(id));
        return await _dataStorage.FindAsync<Settings>(id, cancellationToken).ConfigureAwait(false);
    }

    public Task<Settings?> GetByChatIdAsync(string chatId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chatId, nameof(chatId));
        return Task.Run(
            () => _dataStorage.AsQueryable<Settings>().SingleOrDefault(v => v.ChatId == chatId),
            cancellationToken);
    }

    public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id, nameof(id));

        await _dataStorage.RemoveAsync<Settings>(id, cancellationToken).ConfigureAwait(false);
        _logger.LogDebug("Settings with id {Id} removed.", id);
    }
}
