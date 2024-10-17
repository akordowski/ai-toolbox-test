namespace AIToolbox.Agents.ChatCompletion.Models;

public sealed record Message
{
    public required string Id { get; init; } = default!;
    public required string ChatId { get; init; } = default!;
    public required string? UserId { get; init; } = default!;
    public string? Content { get; set; }
    public required AuthorRole Role { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
}
