using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryBuilder IncludePostgresMemoryStore(
        this IMemoryBuilder builder,
        PostgresMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.Postgres = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.Postgres,
            nameof(MemoryStoreOptions.Postgres),
            $"No '{nameof(PostgresMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, PostgresMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryBuilder IncludePostgresMemoryStore(
        this IMemoryBuilder builder,
        Action<PostgresMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new PostgresMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludePostgresMemoryStore(options);
    }
}
