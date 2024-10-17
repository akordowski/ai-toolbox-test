using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public class SimpleMemoryStoreFactory : IMemoryStoreFactory
{

    public IMemoryStore GetMemoryStore(
        MemoryStoreOptions options,
        ILoggerFactory? loggerFactory = null,
        HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        var opt = options.SimpleMemoryStore;

        return opt is null
            ? throw new InvalidOperationException($"No '{nameof(SimpleMemoryStoreOptions)}' provided.")
            : new SimpleMemoryStore(opt, loggerFactory);
    }
}
