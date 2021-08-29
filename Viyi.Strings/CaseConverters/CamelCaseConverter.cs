namespace Viyi.Strings.CaseConverters {
    internal sealed class CamelCaseConverter : ICaseConverter {
        public string Convert(string name) {
            if (string.IsNullOrWhiteSpace(name)) { return name; }

            return RegularExpressions.SplitingRegex.Replace(
                RegularExpressions.LowerRegex.IsMatch(name) ? name : name.ToLower(),
                m => m.Index == 0
                    ? m.Groups[1].Value.ToLower()
                    : m.Groups[1].Value.ToUpper()
            );
        }
    }
}
