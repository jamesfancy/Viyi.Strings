using System;
using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.CaseConverters {
    public sealed class LambdaCaseConverter : ICaseConverter {
        readonly Func<string?, string?> convert;

        public LambdaCaseConverter(Func<string?, string?> convert) {
            this.convert = convert ?? throw new ArgumentNullException(nameof(convert));
        }

        [return: NotNullIfNotNull("value")]
        public string? Convert(string? value) => convert(value);
    }
}
