using Xunit.Abstractions;

namespace Xunit.DependencyInjection;

public abstract class BaseTest : BaseDisposable
{
    public ITestOutputHelper? TestOutputHelper { get; }

    protected BaseTest(ITestOutputHelper? testOutputHelper = null)
    {
        TestOutputHelper = testOutputHelper;
    }
}
