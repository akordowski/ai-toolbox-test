using AIToolbox.Options;
using AIToolbox.Options.Connectors;
using AIToolbox.Options.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

internal sealed class BuilderService : IBuilderService
{
    private readonly AIToolboxOptions _options;
    private readonly IServiceCollection _services;

    public BuilderService(
        AIToolboxOptions options,
        IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        _options = options;
        _services = services;
    }

    public IConnectorsBuilder AddConnectors(ConnectorOptions? options = null)
    {
        _options.Connectors ??= new ConnectorOptions();

        if (options is not null)
        {
            _options.Connectors = options;
        }

        return new ConnectorsBuilder(_options.Connectors!, _services, this);
    }

    public IConnectorsBuilder AddConnectors(Action<ConnectorOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Connectors ??= new ConnectorOptions();
        optionsAction(_options.Connectors);

        return AddConnectors(_options.Connectors!);
    }

    public IKernelBuilder AddKernel(KernelOptions? options = null)
    {
        if (options is not null)
        {
            _options.Kernel = options;
        }

        return new KernelBuilder(_options.Kernel!, _services, this);
    }

    public IKernelBuilder AddKernel(Action<KernelOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Kernel ??= new KernelOptions();
        optionsAction(_options.Kernel);

        return AddKernel(_options.Kernel!);
    }

    public IMemoryBuilder AddMemory(MemoryOptions? options = null)
    {
        if (options is not null)
        {
            _options.Memory = options;
        }

        return new MemoryBuilder(_options.Memory!, _services, this);
    }

    public IMemoryBuilder AddMemory(Action<MemoryOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Memory ??= new MemoryOptions();
        optionsAction(_options.Memory);

        return AddMemory(_options.Memory!);
    }

    public IAgentsBuilder AddAgents(AgentOptions? options = null)
    {
        _options.Agents ??= new AgentOptions();

        if (options is not null)
        {
            _options.Agents = options;
        }

        return new AgentsBuilder(_options.Agents!, _services);
    }

    public IAgentsBuilder AddAgents(Action<AgentOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        _options.Agents ??= new AgentOptions();
        optionsAction(_options.Agents);

        return AddAgents(_options.Agents!);
    }
}
