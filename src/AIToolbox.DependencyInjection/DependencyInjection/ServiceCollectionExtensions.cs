using AIToolbox.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AIToolbox.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IAIToolboxBuilder AddAIToolbox(this IServiceCollection services) =>
        services.AddAIToolbox(new AIToolboxOptions());

    public static IAIToolboxBuilder AddAIToolbox(
        this IServiceCollection services,
        AIToolboxOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        return new AIToolboxBuilder(new BuilderService(options, services));
    }

    public static IAIToolboxBuilder AddAIToolbox(
        this IServiceCollection services,
        Action<AIToolboxOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

        var options = new AIToolboxOptions();
        optionsAction.Invoke(options);

        return services.AddAIToolbox(options);
    }

    public static IAIToolboxBuilder AddAIToolbox(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = "AIToolbox") =>
        services.AddAIToolbox(options => configuration.GetRequiredSection(sectionName).Bind(options));
}
