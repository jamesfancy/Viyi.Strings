using Viyi.Strings.Extensions;

namespace Viyi.Strings;

public static class Booleans {
    public static readonly IReadOnlyList<string> Truthy = ["true", "yes", "on"];
    public static readonly IReadOnlyList<string?> Falsy = ["false", "no", "off", "", null];

    public static Func<string?, bool?> CreatePredicator(
        string? truthyString,
        string? falsyString
    ) => str => str.ToBoolean(truthyString, falsyString);

    public static Func<string?, bool?> CreatePredicator(
        IEnumerable<string?> truthyStrings,
        IEnumerable<string?> falsyStrings
    ) => str => str.ToBoolean(truthyStrings, falsyStrings);

    public static Func<string?, bool> CreatePredicator(
        bool targetValue,
        string?[] targetStrings
    ) => str => str.ToBoolean(targetValue, targetStrings);
}
