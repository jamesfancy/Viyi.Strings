using System.Linq;
using System.Text.RegularExpressions;
using Viyi.Strings.Extensions;

namespace Viyi.Strings;

[Obsolete("The name 'NumsRange' is misleading. Use Viyi.Strings.NumSeries instead.")]
public class NumsRange : NumSeries {
    [Obsolete("use ToSeriesString to instead")]
    public static string ToRangeString(
        IEnumerable<int> nums,
        Func<int, string> numberFormatter,
        Func<int, int, string>? serialFormatter = null,
        string separator = ","
    ) => ToSeriesString(nums, numberFormatter, serialFormatter, separator);

    [Obsolete("use ToSeriesString to instead")]
    public static string ToRangeString(
        IEnumerable<int> nums,
        string prefix = "",
        string between = "~",
        string separator = ","
    ) => ToSeriesString(nums, prefix, between, separator);
}

public partial class NumSeries {
#if NET8_0
    [GeneratedRegex("(-?\\d+)")]
    private static partial Regex GenerateIntRegex();
    private static readonly Regex IntRegex = GenerateIntRegex();
#else
    private static readonly Regex IntRegex = new("(-?\\d+)", RegexOptions.Compiled);
#endif

    static readonly string[] DefaultSeparators = [",", ";", " "];
    static readonly string[] DefaultRangeLinker = ["-", "~"];

    public static int[] Parse(string series, bool suppressException = true) {
        try {
            return new NumSeries().Parse(series);
        }
        catch when (suppressException) {
            return [];
        }
    }

    public static string ToSeriesString(
        IEnumerable<int> nums,
        Func<int, string> numberFormatter,
        Func<int, int, string>? serialFormatter = null,
        string separator = ","
    ) {
        serialFormatter ??= (first, last) => $"{numberFormatter(first)}~{numberFormatter(last)}";

        List<(int, int)> segments = [];
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

    public static string ToSeriesString(
        IEnumerable<int> nums,
        string prefix = "",
        string between = "~",
        string separator = ","
    ) {
        return ToSeriesString(
            nums,
            n => $"{prefix}{n}",
            (first, last) => $"{prefix}{first}{between}{prefix}{last}",
            separator
        );
    }

    public Func<string, string[]> Split { get; set; }
    public Func<string, string[]> ExtractRange { get; set; }

    [Obsolete("spell error, use Splitter to instead")]
    public Func<string, string[]> Spliter { get => Split; set => Split = value; }

    [Obsolete("use ExtractRange to instead")]
    public Func<string, string[]> Unpair { get => ExtractRange; set => ExtractRange = value; }

    public Func<string, int> ToInt { get; set; }

    public string[]? Separators { get; set; }
    public bool InOrder { get; set; } = true;

    public NumSeries() {
        Split = InternalSplit;
        ExtractRange = InternalExtractRange;
        ToInt = InternalToInt;
    }

    public bool TryParse(string range, out int[] nums) {
        try {
            nums = Parse(range);
            return true;
        }
        catch {
            nums = [];
            return false;
        }
    }

    public int[] Parse(string range) {
        if (range.IsSpaces()) { return []; }

        IEnumerable<int> series = Split(range)
           .SelectMany(segment => {
               var entries = ExtractRange(segment).Select(ToInt).ToArray();
               return entries.Length switch {
                   0 => [],
                   1 => entries,
                   _ => Enumerable.Range(entries[0], entries[1] - entries[0] + 1)
               };
           });

        return InOrder ? toOrderedArray(series) : [.. series];

#if NET8_0
        static int[] toOrderedArray(IEnumerable<int> series) {
            return [.. series.Order().Distinct()];
        }
#else
        static int[] toOrderedArray(IEnumerable<int> series) {
            var result = series.Distinct().ToArray();
            Array.Sort(result);
            return result;
        }
#endif
    }

    private string[] InternalSplit(string range) {
        return range.Split(Separators ?? DefaultSeparators, StringSplitOptions.RemoveEmptyEntries);
    }

    private string[] InternalExtractRange(string segment) {
        return segment.Split(DefaultRangeLinker, 2, StringSplitOptions.RemoveEmptyEntries);
    }

    private int InternalToInt(string num) {
        var match = IntRegex.Match(num);
        return match.Success
            ? int.Parse(match.Value)
            : throw new FormatException($"cannot parse integer from '{num}'");
    }
}
