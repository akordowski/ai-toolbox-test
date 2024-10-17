namespace AIToolbox.Data;

public interface IDataStorage
{
    Task AddOrUpdateAsync<TValue>(
        TValue value,
        Func<TValue, string> keyFactory,
        CancellationToken cancellationToken = default)
        where TValue : notnull;

    Task AddOrUpdateRangeAsync<TValue>(
        IEnumerable<TValue> values,
        Func<TValue, string> keyFactory,
        CancellationToken cancellationToken = default)
        where TValue : notnull;

    IAsyncEnumerable<TValue> AsAsyncEnumerable<TValue>();

    IQueryable<TValue> AsQueryable<TValue>();

    Task<TValue?> FindAsync<TValue>(
        string key,
        CancellationToken cancellationToken = default);

    IAsyncEnumerator<TValue> GetAsyncEnumerator<TValue>(CancellationToken cancellationToken = default);

    Task RemoveAsync<TValue>(
        string key,
        CancellationToken cancellationToken = default);

    Task RemoveRangeAsync<TValue>(
        IEnumerable<string> keys,
        CancellationToken cancellationToken = default);
}
