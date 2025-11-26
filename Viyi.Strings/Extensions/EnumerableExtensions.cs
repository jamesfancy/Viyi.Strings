using System.Runtime.CompilerServices;
using System.Text;

namespace Viyi.Strings.Extensions;

public static class EnumerableExtensions {
    extension<T>(IEnumerable<T> enumerable) {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string JoinString(char separator) {
#if NETSTANDARD2_0
            return string.Join(separator.ToString(), enumerable);
#else
            return string.Join(separator, enumerable);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string JoinString(string separator) => string.Join(separator, enumerable);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConcatString() => string.Concat(enumerable);
    }

    extension(IEnumerable<char> chars) {
        public string MakeString() {
            if (chars is char[] array) { return new string(array); }
            var builder = new StringBuilder();
            foreach (var ch in chars) { builder.Append(ch); }
            return builder.ToString();
        }
    }
}
