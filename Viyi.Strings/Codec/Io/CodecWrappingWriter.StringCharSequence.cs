using System;
using System.IO;

#if NET5_0_OR_GREATER
namespace Viyi.Strings.Codec.Io
{
    public partial class CodecWrappingWriter
    {
        class StringCharSequence : CharSequance
        {
            readonly string source;

            public StringCharSequence(string data) : base(0, data.Length)
            {
                source = data;
            }

            protected override void Write(TextWriter writer, int offset, int count) =>
                writer.Write(this.source.AsSpan(offset, count));
        }
    }
}
#endif
