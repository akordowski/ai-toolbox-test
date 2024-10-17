namespace AIToolbox;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

public static class EnumerableExtensions
{
    public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> source)
    {
        foreach (var item in source)
        {
            yield return item;
        }
    }
}
