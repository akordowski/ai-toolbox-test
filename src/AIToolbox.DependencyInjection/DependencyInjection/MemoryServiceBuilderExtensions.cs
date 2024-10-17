using AIToolbox.Options.SemanticKernel;
using AIToolbox.SemanticKernel.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public static class MemoryServiceBuilderExtensions
{
    public static IMemoryBuilder IncludeSimpleMemoryStore(
        this IMemoryBuilder builder,
        SimpleMemoryStoreOptions? options = null)
    {
        var opt = builder.Options;

        if (options is not null)
        {
            opt.Store ??= new MemoryStoreOptions();
            opt.Store.SimpleMemoryStore = options;
        }

        Verify.ThrowInvalidOperationExceptionIfNull(
            opt.Store?.SimpleMemoryStore,
            $"No '{nameof(SimpleMemoryStoreOptions)}' provided.");

        builder.Services.AddSingleton<IMemoryStoreFactory, SimpleMemoryStoreFactory>();

        return builder;
    }

    public static IMemoryBuilder IncludeSimpleMemoryStore(
        this IMemoryBuilder builder,
        Action<SimpleMemoryStoreOptions> optionsAction)
    {
        Verify.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new SimpleMemoryStoreOptions();
        optionsAction(options);

        return builder.IncludeSimpleMemoryStore(options);
    }
}
