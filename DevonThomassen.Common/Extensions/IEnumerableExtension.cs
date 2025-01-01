namespace DevonThomassen.Common.Extensions;

public static class EnumerableExtension
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable) 
        => enumerable is null || !enumerable.Any();
}