using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public interface ISemanticTextMemoryRetriever
{
    IAsyncEnumerable<MemoryQueryResult> SearchMemoriesAsync(
        string collection,
        string query,
        int limit = 1,
        double minRelevanceScore = 0.7,
        bool withEmbeddings = false,
        Kernel? kernel = null,
        CancellationToken cancellationToken = default);
}
