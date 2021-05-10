using System.IO;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Io
{
    public class CodecTextReader : CodecAccessor, ICodecTextReader
    {
        protected TextReader Reader { get; }

        public CodecTextReader(TextReader reader, CodecOptions options)
            : base(options)
        {
            this.Reader = reader;
        }

        public virtual int Read(char[] buffer, int start, int count) =>
            Reader.Read(buffer, start, count);

        public virtual int Read(char[] buffer) => Read(buffer, 0, buffer.Length);

        public virtual string? ReadLine() => Reader.ReadLine();

        public virtual string ReadAll() => Reader.ReadToEnd();
    }
}
