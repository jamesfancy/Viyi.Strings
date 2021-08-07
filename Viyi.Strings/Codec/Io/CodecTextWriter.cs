using System.IO;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Io {
    public class CodecTextWriter : CodecAccessor, ICodecTextWriter {
        protected TextWriter Writer { get; }

        public CodecTextWriter(TextWriter writer, CodecOptions? options = null)
            : base(options) {
            this.Writer = writer;
        }

        public virtual void Write(char[] data) => Writer.Write(data);

        public virtual void Write(char[] data, int start, int length) =>
            Writer.Write(data, start, length);

        public virtual void Write(string data) => Writer.Write(data);
    }
}
