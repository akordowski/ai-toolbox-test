using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

public sealed class KernelBuilderConfigurator<T> : IKernelBuilderConfigurator where T : class
{
    private readonly Func<IServiceProvider, T>? _factory;
    private readonly T? _instance;

    public KernelBuilderConfigurator(Func<IServiceProvider, T> factory)
    {
        _factory = factory;
    }

    public KernelBuilderConfigurator(T instance)
    {
        _instance = instance;
    }

    public void Configure(IKernelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        if (_factory is not null)
        {
            builder.Services.AddSingleton(sp => _factory(sp));
        }
        else if (_instance is not null)
        {
            builder.Services.AddSingleton(_instance);
        }
    }
}
