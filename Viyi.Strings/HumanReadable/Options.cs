namespace Viyi.Strings.HumanReadable;

public class Options {
    public const int MaxLengthLimit = 7;
    public const int DefaultMaxLength = 6;
    public const int DefaultDecimal = 2;

    public enum Steps {
        By1024 = 0,
        By1000 = 1,
    }

    private static readonly string[] defaultUnits = new[] { "", "K", "M", "G", "T", "P", "E" };
    public static readonly IReadOnlyList<string> DefaultUnits = defaultUnits;
    public static IReadOnlyList<string> DefaultUnitsWithSpace =>
        defaultUnitsWithSpace ??= defaultUnits.Select(it => $" {it}").ToArray();
    private static IReadOnlyList<string>? defaultUnitsWithSpace;

    public string BasicUnit { get; set; } = "B";
    public IReadOnlyList<string> Units { get; set; } = DefaultUnits;
    public Steps Step { get; set; }
    public int MaxLength { get; set; } = DefaultMaxLength;
    public int Decimal { get; set; } = DefaultDecimal;
}
