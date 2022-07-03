using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64;

public class Base64Codec : CodecBase {
    public override ITextDecoder CreateDecoder(CodecOptions? options = null) {
        options ??= Base64CodecOptions.CreateDefault();
        Schemes scheme = options is Base64CodecOptions b64Options
            ? b64Options.Scheme
            : Schemes.Base64;

        return scheme switch {
            Schemes.Base64Url => new Base64UrlDecoder(options),
            Schemes.Compatible => new Base64CompatibleDecoder(options),
            _ => new Base64Decoder(options),
        };
    }

    public override ITextEncoder CreateEncoder(CodecOptions? options = null) {
        options ??= Base64CodecOptions.CreateDefault();
        Schemes scheme = options is Base64CodecOptions b64Options
            ? b64Options.Scheme
            : Schemes.Base64;

        return scheme switch {
            Schemes.Base64 => new Base64Encoder(options),
            _ => new Base64UrlEncoder(options),
        };
    }
}

public class Base64UrlCodec : CodecBase {
    public override ITextDecoder CreateDecoder(CodecOptions? options = null) {
        options ??= Base64CodecOptions.Create().UseScheme(Schemes.Base64Url).Build();
        return new Base64UrlDecoder(options);
    }

    public override ITextEncoder CreateEncoder(CodecOptions? options = null) {
        options ??= Base64CodecOptions.Create().UseScheme(Schemes.Base64Url).Build();
        return new Base64UrlEncoder(options);
    }
}
