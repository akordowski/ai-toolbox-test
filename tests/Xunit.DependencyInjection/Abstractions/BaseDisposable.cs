namespace Xunit.DependencyInjection;

public abstract class BaseDisposable : IDisposable, IAsyncDisposable
{
    private bool _disposed;
    private bool _disposedAsync;

    protected virtual void Cleanup() { }

    protected virtual ValueTask CleanupAsync() => ValueTask.CompletedTask;

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            Cleanup();
        }

        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposedAsync)
        {
            return;
        }

        await CleanupAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
        _disposedAsync = true;
    }
}
