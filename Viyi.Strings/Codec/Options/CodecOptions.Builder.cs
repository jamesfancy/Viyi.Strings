namespace Viyi.Strings.Codec.Options
{
    public partial class CodecOptions
    {
        public class Builder
        {
            public CodecOptions CodecOptions { get; }

            private static CodecOptions Clone(CodecOptions proto)
            {
                return new CodecOptions
                {
                    LineWidth = proto.LineWidth,
                    EndOfLine = proto.EndOfLine,
                };
            }

            internal Builder(CodecOptions? proto = null)
            {
                CodecOptions = proto == null
                     ? Default
                     : Clone(proto);
            }

            public int LineWidth { set { CodecOptions.LineWidth = value; } }
            public EndOfLines EndOfLine { set { CodecOptions.EndOfLine = value; } }
        }
    }
}
