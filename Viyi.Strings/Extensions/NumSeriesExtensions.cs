namespace Viyi.Strings.Extensions;

public static class NumSeriesExtensions {
    extension(IEnumerable<int> nums) {
        [Obsolete("use ToSeriesString to instead")]
        public string ToRangeString(
            string prefix = "",
            string between = "~",
            string separator = ","
        ) => NumSeries.ToSeriesString(nums, prefix, between, separator);

        [Obsolete("use ToSeriesString to instead")]
        public string ToRangeString(
            Func<int, string> numberFormatter,
            Func<int, int, string>? serialFormatter = null,
            string separator = ","
        ) => NumSeries.ToSeriesString(nums, numberFormatter, serialFormatter, separator);

        public string ToSeriesString(
            string prefix = "",
            string between = "~",
            string separator = ","
        ) => NumSeries.ToSeriesString(nums, prefix, between, separator);

        public string ToSeriesString(
            Func<int, string> numberFormatter,
            Func<int, int, string>? serialFormatter = null,
            string separator = ","
        ) => NumSeries.ToSeriesString(nums, numberFormatter, serialFormatter, separator);
    }
}
