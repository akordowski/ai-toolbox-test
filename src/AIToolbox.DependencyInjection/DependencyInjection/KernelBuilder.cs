using AIToolbox.Options.Agents;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace AIToolbox.DependencyInjection;

internal sealed class KernelBuilder : IKernelBuilder
{
    public KernelOptions Options { get; }
    public IServiceCollection Services { get; }

    private readonly IBuilderService _builderService;

    public KernelBuilder(
        KernelOptions options,
        IServiceCollection services,
        IBuilderService builderService)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(KernelOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));
        Verify.ThrowIfNull(builderService, nameof(builderService));

        Options = options;
        Services = services;

        _builderService = builderService;

        Services
            .AddSingleton(Options)
            .AddSingleton<IKernelProvider, KernelProvider>();
    }

    public IAgentsBuilder AddAgents(AgentOptions? options = null) =>
        _builderService.AddAgents(options);

    public IAgentsBuilder AddAgents(Action<AgentOptions> optionsAction) =>
        _builderService.AddAgents(optionsAction);

    public IMemoryBuilder AddMemory(MemoryOptions? options = null) =>
        _builderService.AddMemory(options);

    public IMemoryBuilder AddMemory(Action<MemoryOptions> optionsAction) =>
        _builderService.AddMemory(optionsAction);

    public IKernelBuilder WithCustomAIServiceSelector(Func<IServiceProvider, IAIServiceSelector> factory)
    {
        Services.AddSingleton<IKernelBuilderConfigurator>(new KernelBuilderConfigurator<IAIServiceSelector>(factory));
        return this;
    }

    public IKernelBuilder WithCustomAIServiceSelector(IAIServiceSelector instance)
    {
        Services.AddSingleton<IKernelBuilderConfigurator>(new KernelBuilderConfigurator<IAIServiceSelector>(instance));
        return this;
    }

    public IKernelBuilder WithCustomFunctionInvocationFilter(Func<IServiceProvider, IFunctionInvocationFilter> factory)
    {
        Services.AddSingleton<IKernelBuilderConfigurator>(new KernelBuilderConfigurator<IFunctionInvocationFilter>(factory));
        return this;
    }

    public IKernelBuilder WithCustomFunctionInvocationFilter(IFunctionInvocationFilter instance)
    {
        Services.AddSingleton<IKernelBuilderConfigurator>(new KernelBuilderConfigurator<IFunctionInvocationFilter>(instance));
        return this;
    }
}
