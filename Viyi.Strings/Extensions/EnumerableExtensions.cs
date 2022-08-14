using System.Collections.Generic;
using System.Text;

namespace Viyi.Strings.Extensions;

public static class EnumerableExtensions {
    public static string JoinString<T>(this IEnumerable<T> enumerable, char separator) {
#if NET6_0_OR_GREATER
        return string.Join(separator, enumerable);
#else
        return string.Join(separator.ToString(), enumerable);
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
