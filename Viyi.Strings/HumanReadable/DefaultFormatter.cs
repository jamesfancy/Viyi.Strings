using Viyi.Strings.Extensions;

namespace Viyi.Strings.HumanReadable;

public class DefaultFormatter : IFormatter {
    private readonly Options options;
    readonly string pattern;
    readonly int intLength;

    internal protected DefaultFormatter(Options options) {
        this.options = options;
        var maxLength = Math.Min(Options.MaxLengthLimit, Math.Max(0, options.MaxLength));
        var @decimal = Math.Max(0, Math.Min(maxLength - 2, options.Decimal));

        intLength = @decimal == 0 ? maxLength : maxLength - @decimal - 1;
        pattern = @decimal == 0 ? "{0}" : $"{{0:0.{'#'.Repeat(@decimal)}}}";
    }

    public string Format(ulong size) {
        var topLimit = (double) Enumerable.Range(0, intLength).Aggregate(1, (last, _) => last * 10);
        double @base = options.Step == Options.Steps.By1000 ? 1000.0 : 1024.0;
        double value = size;
        int level = 0;
        for (; value > topLimit; level++) {
            value /= @base;
        }
        return $"{string.Format(pattern, value)}{options.Units[level]}{options.BasicUnit}";
    }
}
