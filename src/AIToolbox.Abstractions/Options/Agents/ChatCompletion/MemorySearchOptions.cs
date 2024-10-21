namespace AIToolbox.Options.Agents;

public sealed class MemorySearchOptions
{
    public int Limit { get; set; } = 1;
    public double MinRelevanceScore { get; set; } = 0.7;
}
