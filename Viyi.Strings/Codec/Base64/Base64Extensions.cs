using Viyi.Strings.Codec.Extensions;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64
{
    public static class Base64Extensions
    {
        const int DefaultBaseLineWidth = 76;

        public static byte[] DecodeBase64(this string base64)
        {
            return new Base64Codec().Decode(base64);
        }

        public static string EncodeBase64(this string source, int lineWidth = 0)
        {
            return source.DecodeUtf8().EncodeBase64(lineWidth);
        }

        public static string EncodeBase64(this byte[] bytes, CodecOptions? options = null)
        {
            return bytes.IsEmpty()
                ? ""
                : new Base64Codec().Encode(bytes, options);
        }

        public static string EncodeBase64(this byte[] bytes, int lineWidth)
        {
            var options = CodecOptions.Create()
                .SetLineWidth(lineWidth)
                .CodecOptions;
            return EncodeBase64(bytes, options);
        }

        public static string EncodeBase64(this byte[] bytes, bool lineBreak) =>
            lineBreak ? EncodeBase64(bytes, DefaultBaseLineWidth) : EncodeBase64(bytes);

        public static string EncodeBaes64(this string source, bool lineBreak)
        {
            return source.DecodeUtf8().EncodeBase64(lineBreak);
        }
    }
}
