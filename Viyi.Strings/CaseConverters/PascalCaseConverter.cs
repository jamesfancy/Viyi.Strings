using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.CaseConverters;

internal sealed class PascalCaseConverter : ICaseConverter {
    [return: NotNullIfNotNull(nameof(value))]
    public string? Convert(string? value) => Toolkit.ToPascalCase(value);
}
