using System.Text.RegularExpressions;

namespace Viyi.Strings.Extensions;

public static partial class StringExtensions {
#if NET8_0
    [GeneratedRegex(@"^\s*(\d+)\s*([a-z]*)\s*$", RegexOptions.IgnoreCase)]
    private static partial Regex GenerateSimpleRegex();
    private static readonly Regex SimpleRegex = GenerateSimpleRegex();
#else
    private static readonly Regex SimpleRegex = new(@"^\s*(\d+)\s*([a-z]*)\s*$", RegexOptions.IgnoreCase);
#endif

    extension(string? str) {
        public TimeSpan ToTimeSpan(string defaultUnit = "m") {
            if (str is null) { return TimeSpan.Zero; }
            var match = SimpleRegex.Match(str);
            if (!match.Success) { return TimeSpan.Zero; }

            var value = match.Groups[1].Value.ToInt32();
            var unit = match.Groups[2].Value.ToLower().EmptyAs(defaultUnit) switch {
                string u when u.Length == 1 => u[0],
                // NOTE 使用字符 '_' 表示毫秒
                "ms" or "millisecond" or "milliseconds" => '_',
                string u => u[0],
            };

            return unit switch {
                'm' => TimeSpan.FromMinutes(value),
                's' => TimeSpan.FromSeconds(value),
                'h' => TimeSpan.FromHours(value),
                'd' => TimeSpan.FromDays(value),
                'w' => TimeSpan.FromDays(value * 7),
                '_' => TimeSpan.FromMilliseconds(value),
                _ => TimeSpan.Zero,
            };
        }
    }
}
