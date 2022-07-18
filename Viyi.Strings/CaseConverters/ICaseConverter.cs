using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.CaseConverters;

public interface ICaseConverter {
    [return: NotNullIfNotNull("value")]
    string? Convert(string? value);
}
