using System.Linq;

namespace Viyi.Strings.Extensions {
    public static class StringExtensions {
        public static string Repeat(this string str, int count) {
            return Enumerable.Repeat(str, count).ConcatString();
        }

        public static string Repeat(this char ch, int count) {
            return new string(ch, count);
        }
    }
}
