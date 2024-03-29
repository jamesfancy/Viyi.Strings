using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Viyi.Strings.CaseConverters;

public static class Toolkit {
    internal static readonly Regex SplittingRegex = new(
        @"[-_\s]+",
        RegexOptions.ECMAScript
    );

    internal static readonly Regex WordStartRegex = new(
        "[A-Z][a-z]|[A-Z]+(?![a-z])",
        RegexOptions.ECMAScript
    );

    internal static readonly Regex WordStartWithPrefix = new(
        @"(?:^|[-_\s])+([a-z])",
        RegexOptions.ECMAScript
    );

    internal static string ToTransitionString(this string str, string prefix = "-") {
        return WordStartRegex.Replace(str, m => $"{prefix}{m.Groups[0]}").ToLower();
    }

    internal static string ReduceSplitters(this string str, string splitter = "-") {
        return SplittingRegex.Replace(str, splitter);
    }

    [return: NotNullIfNotNull(nameof(value))]
    public static string? ToPascalCase(string? value) {
        if (string.IsNullOrEmpty(value)) { return value; }

        return WordStartWithPrefix.Replace(
            value!.ToTransitionString(),
            m => m.Groups[1].Value.ToUpper()
        );
    }

    [return: NotNullIfNotNull(nameof(value))]
    public static string? ToKebabCase(string? value) {
        if (string.IsNullOrEmpty(value)) { return value; }

        return value!.ToTransitionString("-")
            .ReduceSplitters("-")
            .TrimStart('-');
    }
}
