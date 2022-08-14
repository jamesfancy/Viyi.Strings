using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base32;

sealed class Base32Decoder : Base32DecoderBase {
    public Base32Decoder(CodecOptions options) : base(options, Base32ReverseCharset.Codes) { }

    protected override bool IsValid(char ch) => Base32ReverseCharset.IsValid(ch);
}

sealed class Base32HexDecoder : Base32DecoderBase {
    public Base32HexDecoder(CodecOptions options) : base(options, Base32HexReverseCharset.Codes) { }

    protected override bool IsValid(char ch) => Base32HexReverseCharset.IsValid(ch);
}
