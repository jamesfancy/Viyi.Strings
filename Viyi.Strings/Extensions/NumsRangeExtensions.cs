using System.Collections.Generic;

namespace Viyi.Strings.Extensions;

public static class NumsRangeExtensions {
    public static string ToRangeString(
        this IEnumerable<int> nums,
        string prefix = "",
        string between = "~",
        string separator = ","
    ) => NumsRange.ToRangeString(nums, prefix, between, separator);
}
