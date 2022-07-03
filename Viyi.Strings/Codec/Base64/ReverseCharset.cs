using System;

namespace Viyi.Strings.Codec.Base64;

abstract class ReverseCharset {
    public static Base64ReverseCharset Base64 => base64 ??= new();
    public static Base64UrlReverseCharset Base64Url => base64Url ??= new();
    public static Base64CompatibleReverseCharset Base64Compatible => base64Compatible ??= new();

    static Base64ReverseCharset? base64;
    static Base64UrlReverseCharset? base64Url;
    static Base64CompatibleReverseCharset? base64Compatible;

    protected static readonly int[] AllCodes = {
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
        62, -1, 62, -1, 63,                         // [+ - /]  43~47   (count 5)
        52, 53, 54, 55, 56, 57, 58, 59, 60, 61,     // [0~9]    48~57   (count 10)
        -1, -1, -1, -1, -1, -1, -1,                 // ASCII    58~64   (count 7)
        0, 1, 2, 3, 4, 5, 6,                        // [A~G]    65~71   (count 7)
        7, 8, 9, 10, 11, 12, 13,                    // [H~N]    72~78   (count 7)
        14, 15, 16, 17, 18, 19,                     // [O~T]    79~84   (count 6)
        20, 21, 22, 23, 24, 25,                     // [U~Z]    85~90   (count 6) 
        -1, -1, -1, -1, 63, -1,                     // [    _ ] 91~96   (count 6)
        26, 27, 28, 29, 30, 31, 32,                 // [a~g]    97~103  (count 7)
        33, 34, 35, 36, 37, 38, 39,                 // [h~n]    104~110 (count 7)
        40, 41, 42, 43, 44, 45,                     // [o~t]    111~116 (count 6)
        46, 47, 48, 49, 50, 51,                     // [u~z]    117~122 (count 6) 
    };

    readonly int[] codes;

    protected ReverseCharset(int[] codes) {
        this.codes = codes;
    }

    public bool IsValid(char ch) =>
        ch >= 43 && ch <= 122 && codes[ch] != -1;

    public int ToInt(char ch) => codes[ch];
}

sealed class Base64ReverseCharset : ReverseCharset {
    static readonly int[] base64Codes;

    static Base64ReverseCharset() {
        base64Codes = new int[AllCodes.Length];
        Array.Copy(AllCodes, 0, base64Codes, 0, AllCodes.Length);
        base64Codes['-'] = -1;
        base64Codes['_'] = -1;
    }

    public Base64ReverseCharset() : base(base64Codes) { }
}

sealed class Base64UrlReverseCharset : ReverseCharset {
    static readonly int[] base64UrlCodes;

    static Base64UrlReverseCharset() {
        base64UrlCodes = new int[AllCodes.Length];
        Array.Copy(AllCodes, 0, base64UrlCodes, 0, AllCodes.Length);
        base64UrlCodes['+'] = -1;
        base64UrlCodes['/'] = -1;
    }

    public Base64UrlReverseCharset() : base(base64UrlCodes) { }
}

sealed class Base64CompatibleReverseCharset : ReverseCharset {
    static readonly int[] base64Codes;

    static Base64CompatibleReverseCharset() {
        base64Codes = new int[AllCodes.Length];
        Array.Copy(AllCodes, 0, base64Codes, 0, AllCodes.Length);
    }

    public Base64CompatibleReverseCharset() : base(base64Codes) { }
}
