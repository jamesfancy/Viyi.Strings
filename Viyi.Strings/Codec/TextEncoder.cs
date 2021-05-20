using System;
using System.IO;
using Viyi.Strings.Codec.Io;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec
{
    public abstract class TextEncoder : ITextEncoder
    {
        protected CodecOptions Options { get; }

        protected TextEncoder(CodecOptions options)
        {
            this.Options = options;
        }

        public virtual string Encode(byte[] data)
        {
            using var stream = new MemoryStream(data);
            using var writer = new StringWriter();
            Encode(writer, stream);
            return writer.ToString();
        }

        public virtual void Encode(TextWriter output, Stream input)
        {
            Encode(WrapWriter(output), input);
        }

        protected virtual ICodecTextWriter WrapWriter(TextWriter writer)
        {
            return this.Options.LineWidth > 0
                ? new CodecWrappableWriter(writer, this.Options)
                : new CodecTextWriter(writer, this.Options);
        }

        protected abstract void Encode(ICodecTextWriter writer, Stream input);

        public string Encode(byte[] data, int start, int count)
        {
            throw new NotImplementedException();
        }
    }
}
