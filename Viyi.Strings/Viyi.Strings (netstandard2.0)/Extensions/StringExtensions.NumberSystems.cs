namespace Viyi.Strings.Extensions;

public static partial class StringExtensions {
    public static int ToInt32(this string s, int radix = 10) {
        CheckRadix(radix);
        return s.StartsWith("-")
            ? -(int) ToUInt32(s.Substring(1), radix)
            : (int) ToUInt32(s, radix);
    }

    public static long ToInt64(this string s, int radix = 10) {
        CheckRadix(radix);
        return s.StartsWith("-")
            ? -(long) ToUInt64(s.Substring(1), radix)
            : (long) ToUInt64(s, radix);
    }
}
