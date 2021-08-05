using System;

namespace Viyi.Strings.Codec.Options
{
    public enum LineEndings
    {
        ByEnvironment,
        Lf,
        Crlf,
        Cr,
    }

    public sealed partial class CodecOptions
    {
        public const int NoLineWidth = 0;

        public static Func<CodecOptions>? DefaultCreator { get; set; }
        public static CodecOptions Default => DefaultCreator?.Invoke() ?? new CodecOptions();

        private CodecOptions() { }

        public static Builder Create() => new();
        public static Builder From(CodecOptions prototype) => new(prototype);

        /// <summary>
        /// 如果 LineWidth > 0，按指定的字符数折行
        /// </summary>
        public int LineWidth { get; private set; } = 0;

        /// <summary>
        /// 指定换行符，默认使用 LF（包括在 Windows 下）
        /// </summary>
        public LineEndings LineEnding { get; private set; } = LineEndings.Lf;

        /// <summary>
        /// 对大小写不敏感的编码设置输出字符的大小写，默认为小写。
        /// 配置输出字符的大小写。仅对大小写不敏感的编码方式有效，比如 Hex。
        /// 对大小写敏感的编码方式无效，比如 Base64。
        /// </summary>
        public bool UpperCase { get; private set; } = false;
    }
}
