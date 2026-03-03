namespace Viyi.Strings.HumanReadable;

public static class OptionsExtensions {
    extension(Options options) {
        public Options SetLength(int? maxLength = null, int? @decimal = null) {
            if (maxLength is not null) { options.MaxLength = maxLength.Value; }
            if (@decimal is not null) { options.Decimal = @decimal.Value; }
            return options;
        }

        public Options SetStep(Options.Steps step) {
            options.Step = step;
            return options;
        }

        public Options SetBasicUnit(string? baseUnit = null) {
            options.BasicUnit = baseUnit ?? "";
            return options;
        }

        public Options SeparateBySpace() {
            options.Units = options.Units == Options.DefaultUnits || options.Units == Options.DefaultUnitsWithSpace
                ? Options.DefaultUnitsWithSpace
                : options.Units.Select(it => $" {it}").ToArray();
            return options;
        }
    }
}
