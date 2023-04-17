using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.Extensions;

public static partial class StringExtensions {
    /// <summary>
    /// 扩展方法。将一个字符串重复 count 次组成的新字符串。
    /// 如果当前字符串是 null，不管重复多少次都将得到 ""。
    /// 需要在输入字符串是 null 的时候得到 null，请使用可空链调用运算符 (?.)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static string Repeat(this string? str, int count) {
        return Enumerable.Repeat(str, count).ConcatString();
    }

    public static string Repeat(this char ch, int count) => new(ch, count);

    /// <summary>
    /// 扩展方法。如果当前字符串是 null 或者空字符串 ("")，则返回指定值；
    /// 否则返回当前字符串。
    /// 如果需要保留 null，应该使用可空链运算符调用，如 `str?.EmptyAs()`。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="value">指定用于代替空串的值</param>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? EmptyAs(this string? str, string? value) {
        return string.IsNullOrEmpty(str) ? value : str;
    }

    /// <summary>
    /// 扩展方法。如果当前字符串是 null、空字符串 ("") 或仅含空白字符，则返回指定值；
    /// 否则返回当前字符串。
    /// 如果需要保留 null，应该使用可空链运算符调用，如 `str?.SpacesAs()`。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="value">指定用于代替空白字符串的值</param>
    [return: NotNullIfNotNull(nameof(value))]
    public static string? SpacesAs(this string? str, string? value) {
        return string.IsNullOrWhiteSpace(str) ? value : str;
    }

    /// <summary>
    /// 判断是否空字符串，null 会被判 true。
    /// </summary>
    /// <param name="str"></param>
    public static bool IsEmpty([NotNullWhen(false)] this string? str) => string.IsNullOrEmpty(str);

    /// <summary>
    /// 判断是否空字符串，根据 strict 的值来决定判空时是否包括 null。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="strict">是否严格判断。严格判断时，null 不被认为是空字符串。</param>
    public static bool IsEmpty(this string? str, bool strict) =>
        strict ? str?.Length == 0 : string.IsNullOrEmpty(str);

    /// <summary>
    /// 判断是否空字符串，null 也被判 false。
    /// </summary>
    /// <param name="str"></param>
    public static bool IsNotEmpty([NotNullWhen(true)] this string? str) => !string.IsNullOrEmpty(str);

    /// <summary>
    /// 判断是否空字符串，根据 strict 的值来决定判空时是否包括 null。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="strict">是否严格判断。严格判断时，null 不被认为是空字符串。</param>
    public static bool IsNotEmpty(this string? str, bool strict) => !IsEmpty(str, strict);

    /// <summary>
    /// 判断是否空白字符串，空白字符串包含 Empty 和 null。
    /// </summary>
    /// <param name="str"></param>
    public static bool IsSpaces([NotNullWhen(false)] this string? str) => string.IsNullOrWhiteSpace(str);

    /// <summary>
    /// 判断是否空白字符串，根据 strict 的值来决定判定时是否包含 null 和 string.Empty.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="strict">是否严格判断。严格判断时 null 和 string.Empty 不被认为是空白字符串</param>
    public static bool IsSpaces(this string? str, bool strict) {
        bool isNullOrSpaces = string.IsNullOrWhiteSpace(str);
        return isNullOrSpaces && strict
            ? !string.IsNullOrEmpty(str)
            : isNullOrSpaces;
    }

    /// <summary>
    /// 判断是否空白字符串，Empty 和 null 也会被认为是空白字符串。
    /// </summary>
    /// <param name="str"></param>
    public static bool IsNotSpaces([NotNullWhen(true)] this string? str) => !string.IsNullOrWhiteSpace(str);

    /// <summary>
    /// 判断是否空白字符串，根据 strict 的值来决定判定时是否包含 null 和 string.Empty.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="strict">是否严格判断。严格判断时 null 和 string.Empty 不被认为是空白字符串</param>
    public static bool IsNotSpaces(this string? str, bool strict = false) => !IsSpaces(str, strict);
}
