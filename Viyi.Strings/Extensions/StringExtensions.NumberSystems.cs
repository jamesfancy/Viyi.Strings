using System;
using System.Linq;

namespace Viyi.Strings.Extensions;

public static partial class StringExtensions {
    static class RadixConsts {
        public static readonly char[] CHARS = new[] {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',   // 0 ~ 9
            'a', 'b', 'c', 'd', 'e', 'f',                       // 10 ~ 15
            'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',   // 16 ~ 25
            'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'    // 25 ~ 35
        };

        public static readonly int[] RCHARS = new[] {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,     // 0 ~ 11
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,     // 12 ~ 23
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,     // 24 ~ 31
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,     // 32 ~ 47
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9,                       // '0' ~ '9'
            -1, -1, -1, -1, -1, -1, -1,                         // 58 ~ 64
            10, 11, 12, 13, 14, 15,                             // 'A' ~ 'F'
            16, 17, 18, 19, 20, 21, 22, 23, 24, 25,             // 'G' ~ 'P'
            26, 27, 28, 29, 30, 31, 32, 33, 34, 35,             // 'Q' ~ 'Z'
            -1, -1, -1, -1, -1, -1,                             // 91 ~ 96
            10, 11, 12, 13, 14, 15,                             // 'a' ~ 'f'
            16, 17, 18, 19, 20, 21, 22, 23, 24, 25,             // 'g' ~ 'p'
            26, 27, 28, 29, 30, 31, 32, 33, 34, 35,             // 'q' ~ 'z'
        };
    }

    public static string ToString(this uint value, int radix) {
        CheckRadix(radix);
        if (value == 0u) { return "0"; }
        var r = (uint) radix;
        const int bufferSize = 32;
        char[] buffer = new char[bufferSize];
        int i = bufferSize;
        while (value > 0) {
            buffer[--i] = RadixConsts.CHARS[value % r];
            value /= r;
        }

        return new string(buffer, i, bufferSize - i);
    }

    public static string ToString(this int value, int radix) {
        return value < 0
            ? $"-{ToString((uint) -value, radix)}"
            : ToString((uint) value, radix);
    }

    public static string ToString(this ulong value, int radix) {
        CheckRadix(radix);
        if (value == 0ul) { return "0"; }
        var r = (ulong) radix;
        const int bufferSize = 64;
        char[] buffer = new char[bufferSize];
        int i = bufferSize;
        while (value > 0) {
            buffer[--i] = RadixConsts.CHARS[value % r];
            value /= r;
        }

        return new string(buffer, i, bufferSize - i);
    }

    public static string ToString(this long value, int radix) {
        return value < 0
            ? $"-{ToString((ulong) -value, radix)}"
            : ToString((ulong) value, radix);
    }

    public static uint ToUInt32(this string s, int radix = 10) {
        CheckRadix(radix);
        if (string.IsNullOrEmpty(s)) { return 0u; }
        var factor = 1u;
        var r = (uint) radix;
        return s.Reverse().Aggregate(0u, (n, ch) => {
            n += (uint) RadixConsts.RCHARS[ch] * factor;
            factor *= r;
            return n;
        });
    }

    public static ulong ToUInt64(this string s, int radix = 10) {
        CheckRadix(radix);
        if (string.IsNullOrEmpty(s)) { return 0u; }
        var factor = 1ul;
        var r = (ulong) radix;
        return s.Reverse().Aggregate(0ul, (n, ch) => {
            n += (ulong) RadixConsts.RCHARS[ch] * factor;
            factor *= r;
            return n;
        });
    }

    static void CheckRadix(int radix = 10) {
        if (radix < 2 || radix > 36) {
            throw new ArgumentException($"error radix {radix}, should be in [2, 36]", nameof(radix));
        }
    }
}
