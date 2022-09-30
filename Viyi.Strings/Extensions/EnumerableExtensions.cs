using System.Text;

namespace Viyi.Strings.Extensions;

public static class EnumerableExtensions {
    public static string JoinString<T>(this IEnumerable<T> enumerable, char separator) {
#if NETSTANDARD2_0
        return string.Join(separator.ToString(), enumerable);
#else
        return string.Join(separator, enumerable);
#endif
    }

    public static string JoinString<T>(this IEnumerable<T> enumerable, string separator) {
        return string.Join(separator, enumerable);
    }

    public static string ConcatString<T>(this IEnumerable<T> enumerable) {
        return string.Concat(enumerable);
    }

    public static string MakeString(this IEnumerable<char> chars) {
        if (chars is char[] array) { return new string(array); }
        var builder = new StringBuilder();
        foreach (var ch in chars) { builder.Append(ch); }
        return builder.ToString();
    }
}
