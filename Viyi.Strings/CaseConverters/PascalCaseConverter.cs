using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.CaseConverters {
    internal sealed class PascalCaseConverter : ICaseConverter {
#if NET5_0_OR_GREATER
        [return: NotNullIfNotNull("value")]
#endif
        public string? Convert(string? value) => Toolkit.ToPascalCase(value);
    }
}
