namespace Viyi.Strings.Extensions;

public static partial class StringExtensions {
    private static int MinusOneAs(this int n, int alternate) => n == -1 ? alternate : n;

    public static string Slice(this string str, int startIndex, int endIndex) {
        if (startIndex < 0) { startIndex = str.Length + startIndex; }
        if (endIndex < 0) { endIndex = str.Length + endIndex; }
        if (startIndex >= str.Length || startIndex >= endIndex) { return string.Empty; }
#if NETSTANDARD2_0
        if (endIndex >= str.Length) { return str.Substring(startIndex); }
        return str.Substring(startIndex, endIndex - startIndex);
#else
        if (endIndex >= str.Length) { return str[startIndex..]; }
        return str[startIndex..endIndex];
#endif
    }

    public static string Clice(this string str, Func<string, int> starting, Func<string, int>? ending = null) {
        var startIndex = starting(str).MinusOneAs(0);
        var endIndex = (ending?.Invoke(str) ?? -1).MinusOneAs(str.Length);
#if NETSTANDARD2_0
        return str.Substring(startIndex, endIndex - startIndex);
#else
        return str[startIndex..endIndex];
#endif
    }

    public static string Clice(this string str, Func<string, int> starting, Func<string, int, int> ending) {
        var startIndex = starting(str);
        var endIndex = (ending?.Invoke(str, startIndex) ?? -1).MinusOneAs(str.Length);
        startIndex = startIndex.MinusOneAs(0);
#if NETSTANDARD2_0
        return str.Substring(startIndex, endIndex - startIndex);
#else
        return str[startIndex..endIndex];
#endif
    }

    public static string CliceUntil(this string str, Func<string, int> ending) {
        var endIndex = ending(str);
#if NETSTANDARD2_0
        return endIndex < 0 ? str : str.Substring(0, endIndex);
#else
        return endIndex < 0 ? str : str[..endIndex];
#endif
    }

    public static string SliceUntil(this string str, char ch) {
        var endIndex = str.IndexOf(ch);
#if NETSTANDARD2_0
        return endIndex < 0 ? str : str.Substring(0, endIndex);
#else
        return endIndex < 0 ? str : str[..endIndex];
#endif
    }

    public static string SliceUntil(this string str, string sub) {
        var endIndex = str.IndexOf(sub);
#if NETSTANDARD2_0
        return endIndex < 0 ? str : str.Substring(0, endIndex);
#else
        return endIndex < 0 ? str : str[..endIndex];
#endif
    }
}
