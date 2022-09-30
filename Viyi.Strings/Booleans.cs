using Viyi.Strings.Extensions;

namespace Viyi.Strings;

public static class Booleans {
    public static Func<string?, bool?> CreatePredicator(
        string? trueString, string? falseString) =>
        str => str.ToBoolean(trueString, falseString);

    public static Func<string?, bool?> CreatePredicator(
        string?[] trueStrings, string?[] falseStrings) =>
        str => str.ToBoolean(trueStrings, falseStrings);

    public static Func<string?, bool> CreatePredicator(
        bool value, string?[] valueStrings) =>
        str => str.ToBoolean(value, valueStrings);
}
