using Viyi.Strings.Codec.Abstract;
using Viyi.Strings.Codec.Options;

namespace Viyi.Strings.Codec.Base64
{
    public partial class Base64Codec : CodecBase
    {
        public override ITextDecoder CreateDecoder(CodecOptions? options = null)
        {
            return new Base64Decoder(options ?? CodecOptions.Default);
        }

        public override ITextEncoder CreateEncoder(CodecOptions? options = null)
        {
            return new Base64Encoder(options ?? CodecOptions.Default);
        }
    }
}
