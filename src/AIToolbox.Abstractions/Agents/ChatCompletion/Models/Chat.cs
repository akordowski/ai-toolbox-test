namespace AIToolbox.Agents.ChatCompletion.Models;

public sealed record Chat
{
    public required string Id { get; init; } = default!;
    public required string Title { get; init; } = default!;
    public required DateTimeOffset Timestamp { get; init; }
}
