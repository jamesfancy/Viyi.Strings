using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Viyi.Strings.Extensions;

public static partial class StringExtensions {
    extension(char ch) {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Repeat(int count) => new(ch, count);
    }

    extension(string? str) {
        /// <summary>
        /// 扩展方法。将一个字符串重复 count 次组成的新字符串。
        /// 如果当前字符串是 null，不管重复多少次都将得到 ""。
        /// 需要在输入字符串是 null 的时候得到 null，请使用可空链调用运算符 (?.)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string Repeat(int count) {
            return Enumerable.Repeat(str, count).ConcatString();
        }

        /// <summary>
        /// 扩展方法。如果当前字符串是 null 或者空字符串 ("")，则返回指定值；
        /// 否则返回当前字符串。
        /// 如果需要保留 null，应该使用可空链运算符调用，如 `str?.EmptyAs()`。
        /// </summary>
        /// <param name="value">指定用于代替空串的值</param>
        [return: NotNullIfNotNull(nameof(value))]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string? EmptyAs(string? value) {
            return string.IsNullOrEmpty(str) ? value : str;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string? EmptyAsNull() => string.IsNullOrEmpty(str) ? null : str;

        /// <summary>
        /// 扩展方法。如果当前字符串是 null、空字符串 ("") 或仅含空白字符，则返回指定值；
        /// 否则返回当前字符串。
        /// 如果需要保留 null，应该使用可空链运算符调用，如 `str?.SpacesAs()`。
        /// </summary>
        /// <param name="value">指定用于代替空白字符串的值</param>
        [return: NotNullIfNotNull(nameof(value))]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string? SpacesAs(string? value) {
            return string.IsNullOrWhiteSpace(str) ? value : str;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string? SpacesAsNull() => string.IsNullOrWhiteSpace(str) ? null : str;

        /// <summary>
        /// 判断是否空字符串，根据 strict 的值来决定判空时是否包括 null。
        /// </summary>
        /// <param name="strict">是否严格判断。严格判断时，null 不被认为是空字符串。</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty(bool strict) =>
            strict ? str?.Length == 0 : string.IsNullOrEmpty(str);

        /// <summary>
        /// 判断是否空字符串，根据 strict 的值来决定判空时是否包括 null。
        /// </summary>
        /// <param name="strict">是否严格判断。严格判断时，null 不被认为是空字符串。</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotEmpty(bool strict) => !IsEmpty(str, strict);

        /// <summary>
        /// 判断是否空白字符串，根据 strict 的值来决定判定时是否包含 null 和 string.Empty.
        /// </summary>
        /// <param name="strict">是否严格判断。严格判断时 null 和 string.Empty 不被认为是空白字符串</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsSpaces(bool strict) {
            return strict
                ? string.IsNullOrWhiteSpace(str) && !string.IsNullOrEmpty(str)
                : string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 判断是否空白字符串，根据 strict 的值来决定判定时是否包含 null 和 string.Empty.
        /// </summary>
        /// <param name="strict">是否严格判断。严格判断时 null 和 string.Empty 不被认为是空白字符串</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotSpaces(bool strict = false) => !IsSpaces(str, strict);
    }

    extension([NotNullWhen(false)] string? str) {
        /// <summary>
        /// 判断是否空字符串，null 会被判 true。
        /// </summary>
        /// <remarks>
        /// 相比 string.IsNullOrEmpty()，该方法可以更准确的判断 str 的空状态
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty() => string.IsNullOrEmpty(str);

        /// <summary>
        /// 判断是否空白字符串，空白字符串包含 Empty 和 null。
        /// </summary>
        /// <remarks>
        /// 相比 string.IsNullOrEmpty()，该方法可以更准确的判断 str 的空状态
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsSpaces() => string.IsNullOrWhiteSpace(str);
    }

    extension([NotNullWhen(true)] string? str) {
        /// <summary>
        /// 判断是否空字符串，null 也被判 false。
        /// </summary>
        /// <remarks>
        /// 相比 string.IsNullOrEmpty()，该方法可以更准确的判断 str 的空状态
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotEmpty() => !string.IsNullOrEmpty(str);

        /// <summary>
        /// 判断是否空白字符串，Empty 和 null 也会被认为是空白字符串。
        /// </summary>
        /// <remarks>
        /// 相比 string.IsNullOrEmpty()，该方法可以更准确的判断 str 的空状态
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotSpaces() => !string.IsNullOrWhiteSpace(str);
    }
}
