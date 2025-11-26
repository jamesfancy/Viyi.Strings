using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Viyi.Strings.CaseConverters;

public static partial class Toolkit {
#if NET8_0
    [GeneratedRegex(@"[-_\s]+", RegexOptions.ECMAScript)]
    private static partial Regex GenerateSplittingRegex();
    [GeneratedRegex("[A-Z][a-z]|[A-Z]+(?![a-z])", RegexOptions.ECMAScript)]
    private static partial Regex GenerateWordStartRegex();
    [GeneratedRegex(@"(?:^|[-_\s])+([a-z])", RegexOptions.ECMAScript)]
    private static partial Regex GenerateWordStartWithPrefix();

    internal static readonly Regex SplittingRegex = GenerateSplittingRegex();
    internal static readonly Regex WordStartRegex = GenerateWordStartRegex();
    internal static readonly Regex WordStartWithPrefix = GenerateWordStartWithPrefix();
#else
    const RegexOptions regexOptions = RegexOptions.ECMAScript | RegexOptions.Compiled;
    internal static readonly Regex SplittingRegex = new(@"[-_\s]+", regexOptions);
    internal static readonly Regex WordStartRegex = new("[A-Z][a-z]|[A-Z]+(?![a-z])", regexOptions);
    internal static readonly Regex WordStartWithPrefix = new(@"(?:^|[-_\s])+([a-z])", regexOptions);
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string ToTransitionString(this string str, string prefix = "-") {
        return WordStartRegex.Replace(str, m => $"{prefix}{m.Groups[0]}").ToLower();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
