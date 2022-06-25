namespace Viyi.Strings.Test.Toolkit;

static class EnumerableExtensions {
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
        foreach (var it in enumerable) {
            action(it);
        }
        return enumerable;
    }
}
