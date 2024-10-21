namespace AIToolbox.Options.Agents;

public sealed class ChatHistoryOptions
{
    public string? SystemDescription { get; set; }
    public string? SystemResponse { get; set; }
    public string? InitialAssistantMessage { get; set; }
    public string? InitialMemoryMessage { get; set; }
}
