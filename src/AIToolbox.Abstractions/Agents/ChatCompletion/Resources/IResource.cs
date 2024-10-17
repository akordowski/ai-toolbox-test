namespace AIToolbox.Agents.ChatCompletion.Resources;

public interface IResource<T> where T : notnull
{
    Task AddOrUpdateAsync(
        T value,
        CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(
        Func<T, bool> predicate,
        CancellationToken cancellationToken = default);

    Task<T?> GetAsync(
        string id,
        CancellationToken cancellationToken = default);

    Task RemoveAsync(
        string id,
        CancellationToken cancellationToken = default);
}
