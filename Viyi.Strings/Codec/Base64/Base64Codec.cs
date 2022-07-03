using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64;

public class Base64Codec : CodecBase {
    public override ITextDecoder CreateDecoder(CodecOptions? options = null) {
        return new Base64Decoder(options ?? CodecOptions.CreateDefault());
    }

    public override ITextEncoder CreateEncoder(CodecOptions? options = null) {
        return new Base64Encoder(options ?? CodecOptions.CreateDefault());
    }
}

public class Base64UrlCodec : CodecBase {
    public override ITextDecoder CreateDecoder(CodecOptions? options = null) {
        return new Base64UrlDecoder(options ?? CodecOptions.CreateDefault());
    }

    public override ITextEncoder CreateEncoder(CodecOptions? options = null) {
        return new Base64UrlEncoder(options ?? CodecOptions.CreateDefault());
    }
}
