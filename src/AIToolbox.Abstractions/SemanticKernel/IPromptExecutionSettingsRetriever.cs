using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;

namespace AIToolbox.SemanticKernel;

public interface IPromptExecutionSettingsRetriever
{
    PromptExecutionSettings? GetPromptExecutionSettings(
        Kernel kernel,
        Type serviceType,
        PromptExecutionOptions? options = null);
}
