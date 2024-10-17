using AIToolbox.Options.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IAddKernel
{
    IKernelBuilder AddKernel(KernelOptions? options = null);
    IKernelBuilder AddKernel(Action<KernelOptions> optionsAction);
}
