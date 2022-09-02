using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.CaseConverters;

internal sealed class KebabCaseConverter : ICaseConverter {
    [return: NotNullIfNotNull("value")]
    public string? Convert(string? value) => Toolkit.ToKebabCase(value);
}
