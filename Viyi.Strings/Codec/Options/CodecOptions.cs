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

    public sealed partial class CodecOptions : IEquatable<CodecOptions>
    {
        public const int NoLineWidth = 0;

        public static Func<CodecOptions>? DefaultCreator { get; set; }

        /// <summary>
        /// 预定义的默认配置
        /// </summary>
        public static CodecOptions Default { get; } = new();

        /// <summary>
        /// 由 DefaultCreator 创建的默认配置。
        /// 如果没有指定 DefaultCreator，会使用预定义的默认配置。
        /// </summary>
        public static CodecOptions CreateDefault()
        {
            return DefaultCreator?.Invoke() ?? Default;
        }

        private CodecOptions() { }

        public static Builder Create() => new();
        public static Builder Create(CodecOptions prototype) => new(prototype);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                hash = hash * 113 + LineWidth;
                hash = hash * 113 + (int)LineEnding;
                hash = hash * 113 + (UpperCase ? 37 : 73);
                return hash;
            }
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, this)) { return true; }

            var opts = obj as CodecOptions;
            return Equals(opts);
        }

        public bool Equals(CodecOptions? other)
        {
            if (other == null) { return false; }
            return LineWidth == other.LineWidth
                && LineEnding == other.LineEnding
                && UpperCase == other.UpperCase;
        }

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
