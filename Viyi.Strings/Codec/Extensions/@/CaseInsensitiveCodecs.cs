// 通过 CaseSensitiveCodecs.tt 自动生成的代码，请勿手工修改

namespace Viyi.Strings.Codec.Base32 {

    using System;
    using Viyi.Strings.Codec.Extensions;
    using Viyi.Strings.Codec.Options;

    public static class Base32Extensions {
        const int DefaultBase32LineWidth = 64;

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
            Action<CodecOptions.Builder> building
        ) {
            var optionsBuilder = CodecOptions.Create();
            building?.Invoke(optionsBuilder);
            return EncodeBase32(bytes, optionsBuilder.Build());
        }

        /// <summary>
        /// 对二进制数据进行 Base 32 编码，可以指定字母大小写，以及折行位置。
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="upperCase">是否以大写字母输出。默认为 false，即小写字母输出。</param>
        /// <param name="lineWidth">默认的换行符是 LF，如果需要使用其他换行符，使用自定义的 CodecOptions。</param>
        /// <returns></returns>
        public static string EncodeBase32(this byte[] bytes, bool upperCase, int lineWidth) {
            return EncodeBase32(
                bytes,
                CodecOptions.Create()
                    .UseUpperCase(upperCase)
                    .SetLineWidth(lineWidth)
                    .Build()
            );
        }

        /// <summary>
        /// 对二进制数据进行 Base 32 编码，可以指定字母大小写，以及是否折行。
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="upperCase">是否以大写字母输出。默认为 false，即小写字母。</param>
        /// <param name="lineBreak">
        /// 是否以默认宽度（64 字符）换行，默认为 false。
        /// 默认换行符为 LF，如果需要使用其他换行符，使用自定义的 CodecOptions。
        /// </param>
        /// <returns></returns>
        public static string EncodeBase32(this byte[] bytes, bool upperCase, bool lineBreak = false) {
            return EncodeBase32(
                bytes,
                upperCase,
                lineBreak ? DefaultBase32LineWidth : CodecOptions.NoLineWidth);
        }

        public static string EncodeBase32(this byte[] bytes, int lineWidth) =>
            EncodeBase32(bytes, false, lineWidth);
    }
}

namespace Viyi.Strings.Codec.Base16 {

    using System;
    using Viyi.Strings.Codec.Extensions;
    using Viyi.Strings.Codec.Options;

    public static class Base16Extensions {
        const int DefaultBase16LineWidth = 64;

        public static byte[] DecodeBase16(this string Base16) {
            return new Base16Codec().Decode(Base16);
        }

        public static string EncodeBase16(this byte[] bytes, CodecOptions? options = null) {
            return bytes.IsEmpty()
                ? string.Empty
                : new Base16Codec().Encode(bytes, options);
        }

        public static string EncodeBase16(
            this byte[] bytes,
            Action<CodecOptions.Builder> building
        ) {
            var optionsBuilder = CodecOptions.Create();
            building?.Invoke(optionsBuilder);
            return EncodeBase16(bytes, optionsBuilder.Build());
        }

        /// <summary>
        /// 对二进制数据进行 Base 16 编码，可以指定字母大小写，以及折行位置。
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="upperCase">是否以大写字母输出。默认为 false，即小写字母输出。</param>
        /// <param name="lineWidth">默认的换行符是 LF，如果需要使用其他换行符，使用自定义的 CodecOptions。</param>
        /// <returns></returns>
        public static string EncodeBase16(this byte[] bytes, bool upperCase, int lineWidth) {
            return EncodeBase16(
                bytes,
                CodecOptions.Create()
                    .UseUpperCase(upperCase)
                    .SetLineWidth(lineWidth)
                    .Build()
            );
        }

        /// <summary>
        /// 对二进制数据进行 Base 16 编码，可以指定字母大小写，以及是否折行。
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="upperCase">是否以大写字母输出。默认为 false，即小写字母。</param>
        /// <param name="lineBreak">
        /// 是否以默认宽度（64 字符）换行，默认为 false。
        /// 默认换行符为 LF，如果需要使用其他换行符，使用自定义的 CodecOptions。
        /// </param>
        /// <returns></returns>
        public static string EncodeBase16(this byte[] bytes, bool upperCase, bool lineBreak = false) {
            return EncodeBase16(
                bytes,
                upperCase,
                lineBreak ? DefaultBase16LineWidth : CodecOptions.NoLineWidth);
        }

        public static string EncodeBase16(this byte[] bytes, int lineWidth) =>
            EncodeBase16(bytes, false, lineWidth);
    }
}

