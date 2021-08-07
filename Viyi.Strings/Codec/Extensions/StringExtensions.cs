using System;
using System.Text;

namespace Viyi.Strings.Codec.Extensions {
    public static class StringExtensions {
        public static byte[] DecodeUtf8(this string? str) {
            return str == null
                ? Array.Empty<byte>()
                : Encoding.UTF8.GetBytes(str);
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
}
