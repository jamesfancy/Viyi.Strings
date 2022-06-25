using System;
using Viyi.Strings.Codec.Extensions;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base32 {
    public static class Base32Extensions {
        const int DefaultBaes32LineWidth = 64;

        public static byte[] DecodeBase32(this string Base32) {
            return new Base32Codec().Decode(Base32);
        }

        public static string EncodeBase32(this byte[] bytes, CodecOptions? options = null) {
            return bytes.IsEmpty()
                ? string.Empty
                : new Base32Codec().Encode(bytes, options);
        }

        public static string EncodeBase32(
            this byte[] bytes,
            Action<CodecOptions.Builder> buidling
        ) {
            var optionsBuilder = new CodecOptions.Builder();
            buidling?.Invoke(optionsBuilder);
            return EncodeBase32(bytes, optionsBuilder.Build());
        }

        public static string EncodeBase32(this byte[] bytes, int lineWidth) {
            return EncodeBase32(
                bytes,
                CodecOptions.Create().SetLineWidth(lineWidth).Build()
            );
        }

        public static string EncodeBase32(this byte[] bytes, bool lineBreak) =>
            lineBreak ? EncodeBase32(bytes, DefaultBaes32LineWidth) : EncodeBase32(bytes);

        public static string EncodeBaes64(this string source, bool lineBreak) {
            return source.DecodeUtf8().EncodeBase32(lineBreak);
        }
    }
}
