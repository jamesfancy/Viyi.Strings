namespace Viyi.Strings.Extensions;

public static partial class StringExtensions {
#if NETSTANDARD2_0
    public static int ToInt32(this string s, int radix = 10) {
        return s.StartsWith("-") ? -(int) ToUInt32(s.Substring(1), radix) : (int) ToUInt32(s, radix);
    }

    public static long ToInt64(this string s, int radix = 10) {
        return s.StartsWith("-") ? -(long) ToUInt64(s.Substring(1), radix) : (long) ToUInt64(s, radix);
    }
#else
    public static int ToInt32(this string s, int radix = 10) {
        return s.StartsWith('-') ? -(int) ToUInt32(s[1..], radix) : (int) ToUInt32(s, radix);
    }

    public static long ToInt64(this string s, int radix = 10) {
        return s.StartsWith('-') ? -(long) ToUInt64(s[1..], radix) : (long) ToUInt64(s, radix);
    }
#endif
}
