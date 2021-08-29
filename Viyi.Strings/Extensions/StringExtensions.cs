using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Viyi.Strings.Extensions {
    public static class StringExtensions {
        public static string Repeat(this string str, int count) {
            return Enumerable.Repeat(str, count).ConcatString();
        }

        public static string Repeat(this char ch, int count) {
            return new string(ch, count);
        }

        /// <summary>
        /// 扩展方法。如果当前字符串是 null 或者空字符串 ("")，则返回指定值；
        /// 否则返回当前字符串。
        /// 如果需要保留 null，应该使用可空链运算符调用，如 `str?.EmptyAs()`。
        /// </summary>
        /// <param name="value">指定用于代替空串的值</param>
        [return: NotNullIfNotNull("value")]
        public static string? EmptyAs(this string? str, string? value) {
            return string.IsNullOrEmpty(str) ? value : str;
        }

        /// <summary>
        /// 扩展方法。如果当前字符串是 null、空字符串 ("") 或仅含空白字符，则返回指定值；
        /// 否则返回当前字符串。
        /// 如果需要保留 null，应该使用可空链运算符调用，如 `str?.SpacesAs()`。
        /// </summary>
        /// <param name="value">指定用于代替空白字符串的值</param>
        [return: NotNullIfNotNull("value")]
        public static string? SpacesAs(this string? str, string? value) {
            return string.IsNullOrWhiteSpace(str) ? value : str;
        }
    }
}
