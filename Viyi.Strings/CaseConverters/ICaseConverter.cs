using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.CaseConverters;

public interface ICaseConverter {
    [return: NotNullIfNotNull(nameof(value))]
    string? Convert(string? value);
}
