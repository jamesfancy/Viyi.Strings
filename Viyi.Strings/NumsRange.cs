using System.Text.RegularExpressions;
using Viyi.Strings.Extensions;

namespace Viyi.Strings;

public class NumsRange {
    static readonly string[] DefaultSeparators = new[] { ",", ";", " " };
    static readonly string[] DefaultRangeLinker = new[] { "-", "~" };
    static readonly Regex IntRegex = new("(-?\\d+)");

    public static int[] Parse(string range, bool suppressException = true) {
        try {
            return new NumsRange().Parse(range);
        }
        catch when (suppressException) {
            return Array.Empty<int>();
        }
    }

    public static string ToRangeString(
        IEnumerable<int> nums,
        Func<int, string> numberFormatter,
        Func<int, int, string>? serialFormatter = null,
        string separator = ","
    ) {
        serialFormatter ??= (first, last) => $"{numberFormatter(first)}~{numberFormatter(last)}";

        List<(int, int)> segments = new();
        (int, int) lastSegment;

        using IEnumerator<int> enumerator = nums.GetEnumerator();

        if (!enumerator.MoveNext()) { return ""; }
        lastSegment = (enumerator.Current, enumerator.Current);

        while (enumerator.MoveNext()) {
            var current = enumerator.Current;
            if (current == lastSegment.Item2 + 1) {
                lastSegment.Item2 = current;
                continue;
            }

            segments.Add(lastSegment);
            lastSegment = (current, current);
        }
        segments.Add(lastSegment);

        return segments
            .Select(entry => {
                var (first, last) = entry;
                return first == last ? numberFormatter(last) : serialFormatter(first, last);
            })
            .JoinString(separator);

    }

    public static string ToRangeString(
        IEnumerable<int> nums,
        string prefix = "",
        string between = "~",
        string separator = ","
    ) {
        return ToRangeString(
            nums,
            n => $"{prefix}{n}",
            (first, last) => $"{prefix}{first}{between}{prefix}{last}",
            separator
        );
    }

    public Func<string, string[]> Spliter { get; set; }
    public Func<string, string[]> Unpair { get; set; }
    public Func<string, int> ToInt { get; set; }

    public string[]? Separators { get; set; }
    public bool InOrder { get; set; } = true;

    public NumsRange() {
        Spliter = InternalSplit;
        Unpair = InternalUnpair;
        ToInt = InternalToInt;
    }

    public bool TryParse(string range, out int[] nums) {
        try {
            nums = Parse(range);
            return true;
        }
        catch {
            nums = Array.Empty<int>();
            return false;
        }
    }

    public int[] Parse(string range) {
        if (range.IsSpaces()) { return Array.Empty<int>(); }

        var result = Spliter(range)
            .SelectMany(segment => {
                var entry = Unpair(segment).Select(ToInt).ToArray();
                if (entry.Length == 0) { return Enumerable.Empty<int>(); }
                var end = entry.Length > 1 ? entry[1] : entry[0];
                return Enumerable.Range(entry[0], end - entry[0] + 1);
            })
            .ToArray();

        if (InOrder) { Array.Sort(result); }

        return result;
    }

    private string[] InternalSplit(string range) {
        return range.Split(Separators ?? DefaultSeparators, StringSplitOptions.RemoveEmptyEntries);
    }

    private string[] InternalUnpair(string segment) {
        return segment.Split(DefaultRangeLinker, 2, StringSplitOptions.RemoveEmptyEntries);
    }

    private int InternalToInt(string num) {
        var match = IntRegex.Match(num);
        return match.Success
            ? int.Parse(match.Value)
            : throw new FormatException($"cannot parse interger from '{num}'");
    }
}
