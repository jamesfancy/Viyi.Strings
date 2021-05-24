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
        public int LineWidth { get; private set; }
        public LineEndings LineEnding { get; private set; }

        /// <summary>
        /// 配置输出字符的大小写。仅对大小写不敏感的编码方式有效，比如 Hex。
        /// 对大小写敏感的编码方式无效，比如 Base64。
        /// </summary>
        public bool UpperCase { get; private set; } = true;
    }
}
