using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AIToolbox;

internal static class Verify
{
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null,
        string? message = null)
    {
        if (argument is null)
        {
            throw new ArgumentNullException(paramName, message);
        }
    }

    public static void ThrowInvalidOperationExceptionIfNull(
        [NotNull] object? argument,
        string? message = null)
    {
        if (argument is null)
        {
            throw new InvalidOperationException(message);
        }
    }
}
