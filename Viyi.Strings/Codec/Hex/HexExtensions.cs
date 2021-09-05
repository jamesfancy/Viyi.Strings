using Viyi.Strings.Codec.Extensions;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Hex {
    public static class HexExtensions {
        const int DefaultHexLineWidth = 64;

        public static byte[] DecodeHex(this string hex) {
            return new HexCodec().Decode(hex);
        }

        public static string EncodeHex(this byte[] bytes, CodecOptions? options) {
            return bytes.IsEmpty()
                ? string.Empty
                : new HexCodec().Encode(bytes, options);
        }

        public static string EncodeHex(this byte[] bytes) => EncodeHex(bytes, null);

        /// <summary>
        /// 把二进制数据转换成十六进制文本，可以指定字母大小写，以及折行位置。
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="upperCase">是否以大写字母输出。默认为 false，即小写字母输出。</param>
        /// <param name="lineWidth">默认的换行符是 LF，如果需要使用其他换行符，使用自定义的 CodecOptions。</param>
        /// <returns></returns>
        public static string EncodeHex(this byte[] bytes, bool upperCase, int lineWidth) {
            return EncodeHex(
                bytes,
                CodecOptions.Create()
                    .UseUpperCase(upperCase)
                    .SetLineWidth(lineWidth)
                    .Build()
            );
        }

        /// <summary>
        /// 把二进制数据转换成十六进制文本，可以指定字母大小写，以及是否折行。
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="upperCase">是否以大写字母输出。默认为 false，即小写字母。</param>
        /// <param name="lineBreak">
        /// 是否以默认宽度（76 字符）换行，默认为 false。
        /// 默认换行符为 LF，如果需要使用其他换行符，使用自定义的 CodecOptions。
        /// </param>
        /// <returns></returns>
        public static string EncodeHex(this byte[] bytes, bool upperCase, bool lineBreak = false) {
            return EncodeHex(
                bytes,
                upperCase,
                lineBreak ? DefaultHexLineWidth : CodecOptions.NoLineWidth);
        }

        public static string EncodeHex(this byte[] bytes, int lineWidth) =>
            EncodeHex(bytes, false, lineWidth);
    }
}
