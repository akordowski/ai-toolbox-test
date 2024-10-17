using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public interface IBuilder<out TOptions>
{
    TOptions Options { get; }
    IServiceCollection Services { get; }
}
