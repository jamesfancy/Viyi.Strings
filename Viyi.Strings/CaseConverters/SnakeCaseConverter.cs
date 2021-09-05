using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.CaseConverters {
    internal sealed class SnakeCaseConverter : ICaseConverter {
#if NET5_0_OR_GREATER
        [return: NotNullIfNotNull("value")]
#endif
        public string? Convert(string? value) {
            if (string.IsNullOrEmpty(value)) { return value; }

            return value!.ToTransitionString("_")
                .ReduceSpliters("_")
                .TrimStart('_');
        }
    }
}
