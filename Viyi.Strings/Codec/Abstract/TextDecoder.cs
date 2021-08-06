using System.IO;
using Viyi.Strings.Codec.Io;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Abstract
{
    public abstract class TextDecoder : ITextDecoder
    {
        protected CodecOptions Options { get; }

        protected TextDecoder(CodecOptions options)
        {
            this.Options = options;
        }

        public byte[] Decode(string codes)
        {
            using var reader = new StringReader(codes);
            using var stream = new MemoryStream();
            Decode(stream, reader);
            return stream.ToArray();
        }

        public virtual void Decode(Stream output, TextReader input)
        {
            Decode(output, WrapReader(input));
        }

        protected virtual ICodecTextReader WrapReader(TextReader reader)
        {
            return new CodecTextReader(reader, Options);
        }

        protected abstract void Decode(Stream output, ICodecTextReader reader);
    }
}
