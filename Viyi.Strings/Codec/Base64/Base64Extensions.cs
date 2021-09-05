using Viyi.Strings.Codec.Extensions;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64 {
    public static class Base64Extensions {
        const int DefaultBaes64LineWidth = 76;

        public static byte[] DecodeBase64(this string base64) {
            return new Base64Codec().Decode(base64);
        }

        public static string EncodeBase64(this byte[] bytes, CodecOptions? options = null) {
            return bytes.IsEmpty()
                ? string.Empty
                : new Base64Codec().Encode(bytes, options);
        }

        public static string EncodeBase64(this byte[] bytes, int lineWidth) {
            return EncodeBase64(
                bytes,
                CodecOptions.Create().SetLineWidth(lineWidth).Build()
            );
        }

        public static string EncodeBase64(this byte[] bytes, bool lineBreak) =>
            lineBreak ? EncodeBase64(bytes, DefaultBaes64LineWidth) : EncodeBase64(bytes);

        public static string EncodeBaes64(this string source, bool lineBreak) {
            return source.DecodeUtf8().EncodeBase64(lineBreak);
        }
    }
}
