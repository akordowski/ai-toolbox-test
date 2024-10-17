using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Xunit.DependencyInjection;

public abstract class BaseFixture : BaseDisposable
{
    public IConfigurationRoot Configuration { get; }
    public IServiceCollection Services { get; } = new ServiceCollection();

    private IServiceProvider? _serviceProvider;

    protected BaseFixture()
    {
        Configuration = GetConfiguration();
    }

    public IServiceProvider GetServiceProvider(ITestOutputHelper? testOutputHelper = null)
    {
        if (_serviceProvider is not null)
        {
            return _serviceProvider;
        }

        AddServices();

        return _serviceProvider = Services.BuildServiceProvider();
    }

    protected virtual void AddServices() { }

    protected virtual IEnumerable<ConfigurationFile> GetConfigurationFiles() => [];

    protected override void Cleanup()
    {
        ((ServiceProvider?)_serviceProvider)?.Dispose();
        Services.Clear();
    }

    private IConfigurationRoot GetConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());
        configurationBuilder.AddEnvironmentVariables();

        var configFiles = GetConfigurationFiles().ToList();

        if (configFiles.Count > 0 &&
            configFiles.All(configFile => !string.IsNullOrWhiteSpace(configFile.Path)))
        {
            foreach (var configFile in configFiles)
            {
                configurationBuilder.AddJsonFile(configFile.Path, configFile.Optional, configFile.ReloadOnChange);
            }
        }

        return configurationBuilder.Build();
    }
}
