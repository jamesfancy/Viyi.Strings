using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.CaseConverters {
    public interface ICaseConverter {
#if NET6_0_OR_GREATER
        [return: NotNullIfNotNull("value")]
#endif
        string? Convert(string? value);
    }
}
