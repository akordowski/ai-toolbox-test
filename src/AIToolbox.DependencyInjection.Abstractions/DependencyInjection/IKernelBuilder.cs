using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.DependencyInjection;

public interface IKernelBuilder : IBuilder<KernelOptions>, IAddAgents, IAddMemory
{
    IKernelBuilder WithCustomAIServiceSelector(Func<IServiceProvider, IAIServiceSelector> factory);
    IKernelBuilder WithCustomAIServiceSelector(IAIServiceSelector instance);
    IKernelBuilder WithCustomFunctionInvocationFilter(Func<IServiceProvider, IFunctionInvocationFilter> factory);
    IKernelBuilder WithCustomFunctionInvocationFilter(IFunctionInvocationFilter instance);
}
