using Viyi.Strings.Codec.Base16;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Hex;

public static class HexExtensions {
    /// <summary>参阅 DecodeBase16()</summary>
    public static byte[] DecodeHex(this string hex) => hex.DecodeBase16();

    /// <summary>参阅 EncodeBase16()</summary>
    public static string EncodeHex(this byte[] bytes, CodecOptions? options) => bytes.EncodeBase16(options);

    /// <summary>参阅 EncodeBase16()</summary>
    public static string EncodeHex(this byte[] bytes, Action<CodecOptions.Builder> building) =>
        bytes.EncodeBase16(building);

    /// <summary>参阅 EncodeBase16()</summary>
    public static string EncodeHex(this byte[] bytes) => EncodeHex(bytes, (CodecOptions?) null);

    /// <summary>参阅 EncodeBase16()</summary>
    public static string EncodeHex(this byte[] bytes, bool upperCase, int lineWidth) =>
        bytes.EncodeBase16(upperCase, lineWidth);

    /// <summary>参阅 EncodeBase16()</summary>
    public static string EncodeHex(this byte[] bytes, bool upperCase, bool lineBreak = false) =>
        bytes.EncodeBase16(upperCase, lineBreak);

    /// <summary>参阅 EncodeBase16()</summary>
    public static string EncodeHex(this byte[] bytes, int lineWidth) => EncodeHex(bytes, false, lineWidth);
}
