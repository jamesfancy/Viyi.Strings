namespace Viyi.Strings.Codec.Base64 {
    static class ReverseCharset {
        public static readonly int[] Codes = {
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
            62, -1, -1, -1, 63,                         // [+~/] 43~47 (count 5)
            52, 53, 54, 55, 56, 57, 58, 59, 60, 61,     // [0~9] 48~57 (count 10)
            -1, -1, -1, -1, -1, -1, -1,                 // ASCII 58~64 (count 7)
            0, 1, 2, 3, 4, 5, 6,                        // [A~G] 65~71 (count 7)
            7, 8, 9, 10, 11, 12, 13,                    // [H~N] 72~78 (count 7)
            14, 15, 16, 17, 18, 19,                     // [O~T] 79~84 (count 6)
            20, 21, 22, 23, 24, 25,                     // [U~Z] 85~90 (count 6) 
            -1, -1, -1, -1, -1, -1,                     // ASCII 91~96
            26, 27, 28, 29, 30, 31, 32,                 // [a~g] 97~103 (count 7)
            33, 34, 35, 36, 37, 38, 39,                 // [h~n] 104~110 (count 7)
            40, 41, 42, 43, 44, 45,                     // [o~t] 111~116 (count 6)
            46, 47, 48, 49, 50, 51,                     // [u~z] 117~122 (count 6) 
        };

        public static bool IsValid(char ch) =>
            ch >= 43 && ch <= 122 && Codes[ch] != -1;

        public static int ToInt(char ch) => Codes[ch];
    }
}

