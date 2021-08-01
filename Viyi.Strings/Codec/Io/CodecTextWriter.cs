using System.IO;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Io
{
    public class CodecTextWriter : CodecAccessor, ICodecTextWriter
    {
        protected TextWriter Writer { get; }

        public CodecTextWriter(TextWriter writer, CodecOptions options)
            : base(options)
        {
            this.Writer = writer;
        }

        public virtual void Write(char[] buffer) => Writer.Write(buffer);

        public virtual void Write(char[] buffer, int start, int length) =>
            Writer.Write(buffer, start, length);

        public virtual void Write(string segment) => Writer.Write(segment);
    }
}
