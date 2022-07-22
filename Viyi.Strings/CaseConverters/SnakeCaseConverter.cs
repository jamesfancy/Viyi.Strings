using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.CaseConverters;

internal sealed class SnakeCaseConverter : ICaseConverter {
    [return: NotNullIfNotNull("value")]
    public string? Convert(string? value) {
        if (string.IsNullOrEmpty(value)) { return value; }

        return value!.ToTransitionString("_")
            .ReduceSplitters("_")
            .TrimStart('_');
    }
}
