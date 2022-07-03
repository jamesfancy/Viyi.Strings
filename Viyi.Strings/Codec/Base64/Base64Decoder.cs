using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64;

sealed class Base64Decoder : Base64DecoderBase {
    public Base64Decoder(CodecOptions options) : base(ReverseCharset.Base64, options) { }
}

sealed class Base64UrlDecoder : Base64DecoderBase {
    public Base64UrlDecoder(CodecOptions options) : base(ReverseCharset.Base64Url, options) { }
}

sealed class Base64CompatibleDecoder : Base64DecoderBase {
    public Base64CompatibleDecoder(CodecOptions options) : base(ReverseCharset.Base64Compatible, options) { }
}
