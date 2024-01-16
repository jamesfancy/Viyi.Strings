namespace Viyi.Strings.Extensions;

public static class NumsRangeExtensions {
    public static string ToRangeString(
        this IEnumerable<int> nums,
        string prefix = "",
        string between = "~",
        string separator = ","
    ) => NumsRange.ToRangeString(nums, prefix, between, separator);

    public static string ToRangeString(
        this IEnumerable<int> nums,
        Func<int, string> numberFormatter,
        Func<int, int, string>? serialFormatter = null,
        string separator = ","
    ) => NumsRange.ToRangeString(nums, numberFormatter, serialFormatter, separator);
}
