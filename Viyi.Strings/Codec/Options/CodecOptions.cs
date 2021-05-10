using System;

namespace Viyi.Strings.Codec.Options
{
    public enum EndOfLines
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

        public Builder Create() => new();
        public Builder From(CodecOptions prototype) => new(prototype);

        public int LineWidth { get; private set; }
        public EndOfLines EndOfLine { get; private set; }
    }
}
