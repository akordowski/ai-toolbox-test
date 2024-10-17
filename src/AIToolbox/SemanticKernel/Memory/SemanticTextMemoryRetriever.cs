using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public class SemanticTextMemoryRetriever : ISemanticTextMemoryRetriever
{
    private readonly IMemoryProvider _memoryProvider;

    public SemanticTextMemoryRetriever(IMemoryProvider memoryProvider)
    {
        _memoryProvider = memoryProvider;
    }

    public IAsyncEnumerable<MemoryQueryResult> SearchMemoriesAsync(
        string collection,
        string query,
        int limit = 1,
        double minRelevanceScore = 0.7,
        bool withEmbeddings = false,
        Kernel? kernel = null,
        CancellationToken cancellationToken = default) =>
        _memoryProvider
            .GetMemory()
            .SearchAsync(
                collection,
                query,
                limit,
                minRelevanceScore,
                withEmbeddings,
                kernel,
                cancellationToken);
}
