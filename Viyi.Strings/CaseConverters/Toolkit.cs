using System.Text.RegularExpressions;

namespace Viyi.Strings.CaseConverters {
    static class Toolkit {
        public static readonly Regex SplitingRegex = new(
            @"[-_\s]+",
            RegexOptions.ECMAScript
        );

        public static readonly Regex WordStartRegex = new(
            "[A-Z][a-z]|[A-Z]+(?![a-z])",
            RegexOptions.ECMAScript
        );

        public static readonly Regex WordStartWithPrefix = new(
            @"(?:^|[-_\s])+([a-z])",
            RegexOptions.ECMAScript
        );

        public static string ToTransitionString(this string str, string prefix = "-") {
            return WordStartRegex.Replace(str, m => $"{prefix}{m.Groups[0]}").ToLower();
        }

        public static string ReduceSpliters(this string str, string spliter = "-") {
            return SplitingRegex.Replace(str, spliter);
        }
    }
}
