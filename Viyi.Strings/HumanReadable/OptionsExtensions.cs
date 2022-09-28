namespace Viyi.Strings.HumanReadable;

public static class OptionsExtensions {
    public static Options SetLength(this Options options, int? maxLength = null, int? @decimal = null) {
        if (maxLength != null) { options.MaxLength = maxLength.Value; }
        if (@decimal != null) { options.Decimal = @decimal.Value; }
        return options;
    }

    public static Options SetStep(this Options options, Options.Steps step) {
        options.Step = step;
        return options;
    }

    public static Options SetBasicUnit(this Options options, string? baseUnit = null) {
        options.BasicUnit = baseUnit ?? "";
        return options;
    }

    public static Options SeparateBySpace(this Options options) {
        options.Units = options.Units == Options.DefaultUnits || options.Units == Options.DefaultUnitsWithSpace
            ? Options.DefaultUnitsWithSpace
            : options.Units.Select(it => $" {it}").ToArray();
        return options;
    }
}
