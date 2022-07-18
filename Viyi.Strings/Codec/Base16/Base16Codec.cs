using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base16;

public class Base16Codec : CodecBase {
    public override ITextDecoder CreateDecoder(CodecOptions? options = null) {
        return new Base16Decoder(options ?? CodecOptions.CreateDefault());
    }

    public override ITextEncoder CreateEncoder(CodecOptions? options = null) {
        return new Base16Encoder(options ?? CodecOptions.CreateDefault());
    }
}
