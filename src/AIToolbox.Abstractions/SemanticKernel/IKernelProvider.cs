using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

public interface IKernelProvider
{
    Kernel GetKernel();
}
