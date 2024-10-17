using Microsoft.SemanticKernel.Memory;

namespace AIToolbox.SemanticKernel.Memory;

public interface IMemoryProvider
{
    ISemanticTextMemory GetMemory();
}
