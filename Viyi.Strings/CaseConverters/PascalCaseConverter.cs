namespace Viyi.Strings.CaseConverters {
    internal sealed class PascalCaseConverter : ICaseConverter {
        public string Convert(string name) {
            if (string.IsNullOrWhiteSpace(name)) { return name; }

            return RegularExpressions.SplitingRegex.Replace(
                RegularExpressions.LowerRegex.IsMatch(name) ? name : name.ToLower(),
                m => m.Groups[1].Value.ToUpper()
            );
        }
    }
}
