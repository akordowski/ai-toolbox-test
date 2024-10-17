using AIToolbox.Options;
using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class MemoryBuilder : IMemoryBuilder
{
    public MemoryOptions Options { get; }
    public IServiceCollection Services { get; }

    private readonly IBuilderService _builderService;

    public MemoryBuilder(
        MemoryOptions options,
        IServiceCollection services,
        IBuilderService builderService)
    {
        Verify.ThrowIfNull(options, nameof(options), $"No '{nameof(MemoryOptions)}' provided.");
        Verify.ThrowIfNull(services, nameof(services));
        Verify.ThrowIfNull(builderService, nameof(builderService));

        Options = options;
        Services = services;

        _builderService = builderService;

        Services
            .AddSingleton(Options)
            .AddSingleton<IMemoryProvider, MemoryProvider>();
    }

    public IAgentsBuilder AddAgents(AgentOptions? options = null) =>
        _builderService.AddAgents(options);

    public IAgentsBuilder AddAgents(Action<AgentOptions> optionsAction) =>
        _builderService.AddAgents(optionsAction);
}
