using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Abstract;

public abstract class CodecBase : ITextCodec {
    public abstract ITextDecoder CreateDecoder(CodecOptions? options = null);
    public abstract ITextEncoder CreateEncoder(CodecOptions? options = null);

    public byte[] Decode(string code, CodecOptions? options = null) =>

        CreateDecoder(options).Decode(code);

    public string Encode(byte[] data, CodecOptions? options = null) =>
        CreateEncoder(options).Encode(data);

    public string Encode(byte[] data, int start, int count, CodecOptions? options = null) =>
        CreateEncoder(options).Encode(data, start, count);
}
