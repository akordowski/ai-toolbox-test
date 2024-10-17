using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IAddMemory
{
    IMemoryBuilder AddMemory(MemoryOptions? options = null);
    IMemoryBuilder AddMemory(Action<MemoryOptions> optionsAction);
}
