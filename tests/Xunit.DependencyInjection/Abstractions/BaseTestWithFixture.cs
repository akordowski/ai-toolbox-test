using Xunit.Abstractions;

namespace Xunit.DependencyInjection;

public abstract class BaseTestWithFixture<TFixture> : BaseTest, IClassFixture<TFixture> where TFixture : class
{
    public TFixture Fixture { get; }

    protected BaseTestWithFixture(TFixture fixture, ITestOutputHelper? testOutputHelper = null)
        : base(testOutputHelper)
    {
        ArgumentNullException.ThrowIfNull(fixture, nameof(fixture));

        Fixture = fixture;
    }
}
