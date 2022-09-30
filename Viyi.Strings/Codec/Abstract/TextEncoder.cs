using Viyi.Strings.Codec.Io;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Abstract;

public abstract class TextEncoder : ITextEncoder {
    protected CodecOptions Options { get; }

    protected TextEncoder(CodecOptions options) {
        this.Options = options;
    }

    public virtual string Encode(byte[] data) {
        using var stream = new MemoryStream(data);
        return Encode(stream);
    }

    public string Encode(byte[] data, int start, int count) {
        using var stream = new MemoryStream(data, start, count);
        return Encode(stream);
    }

    public virtual void Encode(TextWriter output, Stream input) {
        Encode(WrapWriter(output), input);
    }

    protected virtual ICodecTextWriter WrapWriter(TextWriter writer) {
        return this.Options.LineWidth > 0
            ? new CodecWrappingWriter(writer, this.Options)
            : new CodecTextWriter(writer, this.Options);
    }

    protected abstract void Encode(ICodecTextWriter writer, Stream input);

    string Encode(Stream stream) {
        using var writer = new StringWriter();
        Encode(writer, stream);
        return writer.ToString();
    }
}
