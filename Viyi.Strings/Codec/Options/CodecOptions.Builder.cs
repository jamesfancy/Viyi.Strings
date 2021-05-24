namespace Viyi.Strings.Codec.Options
{
    partial class CodecOptions
    {
        public class Builder
        {
            public CodecOptions CodecOptions { get; }

            private static CodecOptions Clone(CodecOptions proto)
            {
                return new CodecOptions
                {
                    LineWidth = proto.LineWidth,
                    LineEnding = proto.LineEnding,
                };
            }

            internal Builder(CodecOptions? proto = null)
            {
                CodecOptions = proto == null
                     ? Default
                     : Clone(proto);
            }

            public Builder SetLineWidth(int value)
            {
                CodecOptions.LineWidth = value;
                return this;
            }

            public Builder SetLineEnding(LineEndings value)
            {
                CodecOptions.LineEnding = value;
                return this;
            }

            public Builder UseUpperCase(bool upperCase = true)
            {
                CodecOptions.UpperCase = upperCase;
                return this;
            }

            public Builder UseLowerCase()
            {
                CodecOptions.UpperCase = true;
                return this;
            }
        }
    }
}
