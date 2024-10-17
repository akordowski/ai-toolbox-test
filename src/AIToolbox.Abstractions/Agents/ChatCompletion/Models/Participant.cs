namespace AIToolbox.Agents.ChatCompletion.Models;

public sealed record Participant
{
    public required string Id { get; init; } = default!;
    public required string ChatId { get; init; } = default!;
    public required string UserId { get; init; } = default!;
    public required string UserName { get; init; } = default!;
    public required DateTimeOffset Timestamp { get; init; }
}
