using AIToolbox.Options.SemanticKernel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;

namespace AIToolbox.SemanticKernel;

public sealed class AzureOpenAIPromptExecutionSettingsMapper : IPromptExecutionSettingsMapper
{
    public PromptExecutionSettings? Map(PromptExecutionOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        if (options.AzureOpenAI is null)
        {
            return null;
        }

        var opt = options.AzureOpenAI;
        var toolCallBehavior = opt.ToolCallBehavior switch
        {
            ToolCallBehavior.AutoInvokeKernelFunctions => Microsoft.SemanticKernel.Connectors.OpenAI.ToolCallBehavior.AutoInvokeKernelFunctions,
            ToolCallBehavior.EnableKernelFunctions => Microsoft.SemanticKernel.Connectors.OpenAI.ToolCallBehavior.EnableKernelFunctions,
            null => null,
            _ => throw new ArgumentOutOfRangeException($"Invalid tool call behavior '{opt.ToolCallBehavior}'")
        };

        return new AzureOpenAIPromptExecutionSettings
        {
            ChatSystemPrompt = opt.ChatSystemPrompt,
            FrequencyPenalty = opt.FrequencyPenalty,
            Logprobs = opt.Logprobs,
            MaxTokens = opt.MaxTokens,
            ModelId = opt.ModelId,
            PresencePenalty = opt.PresencePenalty,
            ResponseFormat = opt.ResponseFormat,
            Seed = opt.Seed,
            ServiceId = opt.ServiceId,
            StopSequences = opt.StopSequences,
            Temperature = opt.Temperature,
            TokenSelectionBiases = opt.TokenSelectionBiases,
            ToolCallBehavior = toolCallBehavior,
            TopLogprobs = opt.TopLogprobs,
            TopP = opt.TopP,
            User = opt.User
        };
    }
}