namespace Viyi.Strings.CaseConverters {
    internal sealed class SnakeCaseConverter : ICaseConverter {
        public string Convert(string name) {
            if (string.IsNullOrWhiteSpace(name)) { return name; }

            return RegularExpressions.UpperRegex.Replace(
                RegularExpressions.LowerRegex.IsMatch(name) ? name : name.ToLower(),
                m => "_" + m.Value
            ).Replace('-', '_').ToLower();
        }
    }
}
