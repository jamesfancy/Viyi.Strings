using System;
using System.Text;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Extensions;

public static class StringExtensions {
    public static byte[] DecodeUtf8(this string? str) => Decode(str, Encoding.UTF8);

    public static byte[] Decode(this string? str, Encoding encoding) {
        return str == null
            ? Array.Empty<byte>()
            : encoding.GetBytes(str);
    }

    public static byte[] Decode(this string? str, ITextCodec codec, CodecOptions? options) {
        return str == null
            ? Array.Empty<byte>()
            : codec.Decode(str, options);
    }

    public static byte[] Decode(this string? str, string encoding) {
        if (str == null) { return Array.Empty<byte>(); }

        var codec = TextCodec.CreateOrNull(encoding);
        if (codec != null) {
            return codec.Decode(str);
        }

        return Encoding.GetEncoding(encoding).GetBytes(str);
    }
}
