using System.Runtime.CompilerServices;

namespace Viyi.Strings.Extensions;

public static partial class StringExtensions {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int NegativeOneAs(this int n, int alternate) => n == -1 ? alternate : n;

    extension(string str) {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string InternalSlice(int beginIndex, int endIndex) {
#if NETSTANDARD2_0
            return endIndex >= str.Length
                ? str.Substring(beginIndex)
                : str.Substring(beginIndex, endIndex - beginIndex);
#else
            return endIndex >= str.Length ? str[beginIndex..] : str[beginIndex..endIndex];
#endif
        }

        public string Slice(int beginIndex, int endIndex) {
            if (beginIndex < 0) { beginIndex = str.Length + beginIndex; }
            if (endIndex < 0) { endIndex = str.Length + endIndex; }
            if (beginIndex >= str.Length || beginIndex >= endIndex) { return string.Empty; }
            return str.InternalSlice(beginIndex, endIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Slice(int beginIndex) {
#if NETSTANDARD2_0
            return beginIndex < str.Length ? str.Substring(beginIndex) : string.Empty;
#else
            return beginIndex < str.Length ? str[beginIndex..] : string.Empty;
#endif
        }

        [Obsolete("使用 Slice(int beginIndex, int endIndex) 代替，可提前计算再调用")]
        public string Slice(Func<string, int> starting, Func<string, int>? ending = null) {
            var startIndex = starting(str).NegativeOneAs(0);
            var endIndex = (ending?.Invoke(str) ?? -1).NegativeOneAs(str.Length);
            return str.Slice(startIndex, endIndex);
        }

        [Obsolete("使用 Slice(int beginIndex, int endIndex) 代替，可提前计算再调用")]
        public string Slice(Func<string, int> starting, Func<string, int, int> ending) {
            var beginIndex = starting(str);
            var endIndex = ending.Invoke(str, beginIndex).NegativeOneAs(str.Length);
            return str.Slice(beginIndex.NegativeOneAs(0), endIndex);
        }

        public string SliceUntil(Func<string, int> ending) {
            var endIndex = ending(str);
            return endIndex < 0 ? str : str.Slice(0, endIndex);
        }

        public string SliceUntil(params char[] chs) {
            var endIndex = str.IndexOfAny(chs);
            return endIndex < 0 ? str : str.Slice(0, endIndex);
        }

        public string SliceUntil(int beginIndex, char ch) {
            if (beginIndex >= str.Length) { return string.Empty; }
            var endIndex = str.IndexOf(ch, beginIndex);
            return endIndex < 0 ? str.Slice(beginIndex) : str.Slice(beginIndex, endIndex);
        }

        public string SliceUntil(char ch) {
            var endIndex = str.IndexOf(ch);
            return endIndex < 0 ? str : str.Slice(0, endIndex);
        }

        public string SliceUntil(int beginIndex, string sub) {
            if (beginIndex >= str.Length) { return string.Empty; }
            var endIndex = str.IndexOf(sub, beginIndex);
            return endIndex < 0 ? str.Slice(beginIndex) : str.Substring(beginIndex, endIndex);
        }

        public string SliceUntil(string sub) {
            var endIndex = str.IndexOf(sub);
            return endIndex < 0 ? str : str.Slice(0, endIndex);
        }
    }
}
