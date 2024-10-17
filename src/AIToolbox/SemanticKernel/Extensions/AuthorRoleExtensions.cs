using Microsoft.SemanticKernel.ChatCompletion;

namespace AIToolbox.SemanticKernel;

internal static class AuthorRoleExtensions
{
    public static Agents.ChatCompletion.Models.AuthorRole ToEnum(this AuthorRole authorRole) =>
        authorRole.Label switch
        {
            "system" => Agents.ChatCompletion.Models.AuthorRole.System,
            "assistant" => Agents.ChatCompletion.Models.AuthorRole.Assistant,
            "user" => Agents.ChatCompletion.Models.AuthorRole.User,
            "tool" => Agents.ChatCompletion.Models.AuthorRole.Tool,
            _ => throw new ArgumentOutOfRangeException($"Invalid author role '{authorRole.Label}'")
        };

    public static AuthorRole ToStruct(this Agents.ChatCompletion.Models.AuthorRole authorRole) =>
        authorRole switch
        {
            Agents.ChatCompletion.Models.AuthorRole.System => AuthorRole.System,
            Agents.ChatCompletion.Models.AuthorRole.Assistant => AuthorRole.Assistant,
            Agents.ChatCompletion.Models.AuthorRole.User => AuthorRole.User,
            Agents.ChatCompletion.Models.AuthorRole.Tool => AuthorRole.Tool,
            _ => throw new ArgumentOutOfRangeException($"Invalid author role '{authorRole}'")
        };
}
