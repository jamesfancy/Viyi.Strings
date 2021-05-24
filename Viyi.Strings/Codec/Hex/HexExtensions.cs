using Viyi.Strings.Codec.Extensions;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Hex
{
    public static class HexExtensions
    {
        const int DefaultHexLineWidth = 64;

        public static byte[] DecodeHex(this string hex)
        {
            return new HexCodec().Decode(hex);
        }

        public static string EncodeHex(this byte[] bytes, CodecOptions? options = null)
        {
            return bytes.IsEmpty()
                ? ""
                : new HexCodec().Encode(bytes, options);
        }

        public static string EncodeHex(this string source, CodecOptions? options = null)
        {
            return source.DecodeUtf8().EncodeHex(options);
        }

        public static string EncodeHex(this byte[] bytes, int lineWidth, bool upperCase = true)
        {
            return EncodeHex(
                bytes,
                CodecOptions.Create()
                    .SetLineWidth(lineWidth)
                    .UseUpperCase(upperCase)
                    .CodecOptions
            );
        }

        public static string EncodeHex(this byte[] bytes,
            bool upperCase = true, bool lineBreak = false)
        {

            return EncodeHex(
                bytes,
                lineBreak ? DefaultHexLineWidth : CodecOptions.NoLineWidth,
                upperCase);
        }
    }
}
