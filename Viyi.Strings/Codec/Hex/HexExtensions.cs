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
                ? string.Empty
                : new HexCodec().Encode(bytes, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="upperCase">是否以大写字母输出。默认为 false。</param>
        /// <param name="lineWidth"></param>
        /// <returns></returns>
        public static string EncodeHex(this byte[] bytes, bool upperCase, int lineWidth)
        {
            return EncodeHex(
                bytes,
                CodecOptions.Create()
                    .UseUpperCase(upperCase)
                    .SetLineWidth(lineWidth)
                    .CodecOptions
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="upperCase">是否以大写字母输出。默认为 false。</param>
        /// <param name="lineBreak">是否以默认宽度（76 字符）换行，默认为 false。</param>
        /// <returns></returns>
        public static string EncodeHex(this byte[] bytes, bool upperCase = false, bool lineBreak = false)
        {
            return EncodeHex(
                bytes,
                upperCase,
                lineBreak ? DefaultHexLineWidth : CodecOptions.NoLineWidth);
        }

        public static string EncodeHex(this byte[] bytes, int lineWidth) =>
            EncodeHex(bytes, false, lineWidth);
    }
}
