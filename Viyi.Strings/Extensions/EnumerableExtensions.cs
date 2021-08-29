using System.Collections.Generic;

namespace Viyi.Strings.Extensions {
    public static class EnumerableExtensions {
        public static string JoinString<T>(this IEnumerable<T> enumerable, char separator) {
#if NET5_0_OR_GREATER
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
    }
}
