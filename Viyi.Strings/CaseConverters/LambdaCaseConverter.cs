using System.Diagnostics.CodeAnalysis;

namespace Viyi.Strings.CaseConverters;

public sealed class LambdaCaseConverter(Func<string?, string?> convert) : ICaseConverter {
    readonly Func<string?, string?> convert = convert ?? throw new ArgumentNullException(nameof(convert));

    [return: NotNullIfNotNull(nameof(value))]
    public string? Convert(string? value) => convert(value);
}
