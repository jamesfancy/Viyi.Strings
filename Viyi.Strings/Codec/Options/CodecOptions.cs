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
        public static Func<CodecOptions>? DefaultCreator { get; set; }
        public static CodecOptions Default => DefaultCreator?.Invoke() ?? new CodecOptions();

        private CodecOptions() { }

        public static Builder Create() => new();
        public static Builder From(CodecOptions prototype) => new(prototype);

        public int LineWidth { get; private set; }
        public LineEndings LineEnding { get; private set; }
    }
}
