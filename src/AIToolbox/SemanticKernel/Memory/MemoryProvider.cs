using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public class MemoryProvider : IMemoryProvider
{
    private readonly MemoryOptions _memoryOptions;
    private readonly IEnumerable<IMemoryBuilderConfigurator> _configurators;
    private readonly IMemoryStoreFactory? _memoryStoreFactory;
    private readonly ITextEmbeddingGenerationService? _textEmbeddingGenerationService;
    private readonly ILoggerFactory? _loggerFactory;

    public MemoryProvider(
        MemoryOptions memoryOptions,
        IEnumerable<IMemoryBuilderConfigurator> configurators,
        IMemoryStoreFactory? memoryStoreFactory = null,
        ITextEmbeddingGenerationService? textEmbeddingGenerationService = null,
        ILoggerFactory? loggerFactory = null)
    {
        _memoryOptions = memoryOptions;
        _configurators = configurators;
        _memoryStoreFactory = memoryStoreFactory;
        _textEmbeddingGenerationService = textEmbeddingGenerationService;
        _loggerFactory = loggerFactory;
    }

    public ISemanticTextMemory GetMemory()
    {
        var builder = new MemoryBuilder();

        ConfigureLogging(builder);
        ConfigureMemoryStore(builder);
        ConfigureTextEmbeddingGeneration(builder);

        foreach (var configurators in _configurators)
        {
            configurators.Configure(builder);
        }

        return builder.Build();
    }

    private void ConfigureLogging(MemoryBuilder builder)
    {
        if (_loggerFactory is not null)
        {
            builder.WithLoggerFactory(_loggerFactory);
        }
    }

    private void ConfigureMemoryStore(MemoryBuilder builder)
    {
        var options = _memoryOptions.Store;

        if (options is null || _memoryStoreFactory is null)
        {
            return;
        }

        builder.WithMemoryStore((loggerFactory, httpClient) =>
            _memoryStoreFactory.GetMemoryStore(options, loggerFactory, httpClient));
    }

    private void ConfigureTextEmbeddingGeneration(MemoryBuilder builder)
    {
        if (_textEmbeddingGenerationService is not null)
        {
            builder.WithTextEmbeddingGeneration(_textEmbeddingGenerationService);
        }
    }
}
