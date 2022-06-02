using System;
using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.CaseConverters {
    public sealed class LambdaCaseConverter : ICaseConverter {
        readonly Func<string?, string?> convert;

        public LambdaCaseConverter(Func<string?, string?> convert) {
            this.convert = convert ?? throw new ArgumentNullException(nameof(convert));
        }

#if NET6_0_OR_GREATER
        [return: NotNullIfNotNull("value")]
#endif
        public string? Convert(string? value) => convert(value);
    }
}
