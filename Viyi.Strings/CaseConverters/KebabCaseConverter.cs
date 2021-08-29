namespace Viyi.Strings.CaseConverters {
    internal sealed class KebabCaseConverter : ICaseConverter {
        public string Convert(string name) {
            if (string.IsNullOrWhiteSpace(name)) { return name; }

            return RegularExpressions.UpperRegex.Replace(
                RegularExpressions.LowerRegex.IsMatch(name) ? name : name.ToLower(),
                m => "-" + m.Value
            ).Replace('_', '-').ToLower();
        }
    }
}
