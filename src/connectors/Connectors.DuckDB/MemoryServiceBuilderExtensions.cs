using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryBuilder IncludeDuckDBMemoryStore(
        this IMemoryBuilder builder,
        DuckDBMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.DuckDB = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.DuckDB,
            nameof(MemoryStoreOptions.DuckDB),
            $"No '{nameof(DuckDBMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, DuckDBMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryBuilder IncludeDuckDBMemoryStore(
        this IMemoryBuilder builder,
        Action<DuckDBMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new DuckDBMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeDuckDBMemoryStore(options);
    }
}
