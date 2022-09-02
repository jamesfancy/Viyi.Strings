using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.CaseConverters;

internal sealed class CamelCaseConverter : ICaseConverter {
    [return: NotNullIfNotNull("value")]
    public string? Convert(string? value) {
        if (string.IsNullOrEmpty(value)) { return value; }

        return Toolkit.WordStartWithPrefix.Replace(
            value!.ToTransitionString(),
            m => m.Index == 0
                ? m.Groups[1].Value.ToLower()
                : m.Groups[1].Value.ToUpper());
    }
}
