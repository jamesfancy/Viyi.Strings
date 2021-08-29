using System.Text.RegularExpressions;

namespace Viyi.Strings.CaseConverters {
    static class RegularExpressions {
        public static readonly Regex LowerRegex = new(@"[a-z]");
        public static readonly Regex UpperRegex = new(@"\B[A-Z]");
        public static readonly Regex SplitingRegex = new(@"(?:^[-_]?|[-_]+)([a-zA-Z])");
    }
}
