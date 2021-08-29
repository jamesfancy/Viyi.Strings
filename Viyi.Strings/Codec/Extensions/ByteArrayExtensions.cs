using System.Text;
using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Extensions {
    public static class ByteArrayExtensions {
        public static bool IsEmpty(this byte[] bytes) => bytes.Length == 0;

        public static string EncodeUtf8(this byte[] bytes) => Encode(bytes, Encoding.UTF8);

        public static string EncodeUtf8(this byte[] bytes, int start, int count) =>
            Encode(bytes, start, count, Encoding.UTF8);

        public static string Encode(this byte[] bytes, Encoding encoding) =>
            encoding.GetString(bytes);

        public static string Encode(this byte[] bytes,
            int start, int count,
            Encoding encoding) =>
            encoding.GetString(bytes, start, count);

        public static string Encode(this byte[] bytes, ITextCodec codec, CodecOptions? options) =>
            codec.Encode(bytes, options);

        public static string Encode(this byte[] bytes,
            int start, int count,
            ITextCodec codec, CodecOptions? options) =>
            codec.Encode(bytes, start, count, options);

        public static string Encode(this byte[] bytes, string encoding) {
            if (IsEmpty(bytes)) { return string.Empty; }

            var codec = TextCodec.CreateOrNull(encoding);
            if (codec != null) {
                return codec.Encode(bytes);
            }

            return Encoding.GetEncoding(encoding).GetString(bytes);
        }

        public static string Encode(this byte[] bytes, int start, int count, string encoding) {
            if (IsEmpty(bytes) || count == 0) { return string.Empty; }

            var codec = TextCodec.CreateOrNull(encoding);
            if (codec != null) {
                return codec.Encode(bytes, start, count);
            }

            return Encoding.GetEncoding(encoding)
                .GetString(bytes, start, count);
        }
    }
}
