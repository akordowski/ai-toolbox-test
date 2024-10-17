using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryBuilder IncludeSqlServerMemoryStore(
        this IMemoryBuilder builder,
        SqlServerMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.SqlServer = options;
        }

        Verify.ThrowIfNull(
            opt.Store?.SqlServer,
            nameof(MemoryStoreOptions.SqlServer),
            $"No '{nameof(SqlServerMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, SqlServerMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryBuilder IncludeSqlServerMemoryStore(
        this IMemoryBuilder builder,
        Action<SqlServerMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new SqlServerMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeSqlServerMemoryStore(options);
    }
}
