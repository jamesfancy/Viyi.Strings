namespace Viyi.Strings.Test.Toolkit;

static class RandomExtensions {
    static readonly char[] LOWER_LETTERS = Enumerable.Range('a', 26).Select(n => (char) n).ToArray();
    static readonly char[] UPPER_LETTERS = Enumerable.Range('A', 26).Select(n => (char) n).ToArray();
    static readonly char[] NUMBERS = Enumerable.Range('0', 10).Select(n => (char) n).ToArray();
    static readonly char[] BASE62_CHARS = LOWER_LETTERS.Union(UPPER_LETTERS).Union(NUMBERS).ToArray();
    static readonly char[] BASE64_CHARS = BASE62_CHARS.Union(new[] { '/', '+' }).ToArray();

    static readonly char[] BASE32_CHARS = Enumerable.Range('2', 6)
        .Select(n => (char) n)
        .Union(LOWER_LETTERS)
        .Union(UPPER_LETTERS).ToArray();

    static readonly char[] BASE32_HEX_CHARS = Enumerable.Range('0', 10)
        .Select(n => (char) n)
        .Union(LOWER_LETTERS.Take(22))
        .Union(UPPER_LETTERS.Take(22)).ToArray();

    static readonly char[] HEX_CHARS = NUMBERS
        .Union("abcdef".ToCharArray())
        .Union("ABCDEF".ToCharArray())
        .ToArray();

    public static string RandomString(this Random random, int length, char[]? charset = null) {
        charset ??= BASE62_CHARS;
        int count = charset.Length;
        char[] buffer = new char[length];
        for (int i = 0; i < length; i++) {
            buffer[i] = charset[random.Next(count)];
        }

        return new string(buffer);
    }

    public static string RandomBase32(this Random random, int length) =>
        RandomString(random, length, BASE32_CHARS);

    public static string RandomBase32Hex(this Random random, int length) =>
        RandomString(random, length, BASE32_HEX_CHARS);

    public static string RandomBase64(this Random random, int length) =>
        RandomString(random, length, BASE64_CHARS);

    public static string RandomHex(this Random random, int length) =>
        RandomString(random, length, HEX_CHARS);

    public static byte[] RandomBytes(this Random random, int length) {
        byte[] buffer = new byte[length];
        random.NextBytes(buffer);
        return buffer;
    }
}
